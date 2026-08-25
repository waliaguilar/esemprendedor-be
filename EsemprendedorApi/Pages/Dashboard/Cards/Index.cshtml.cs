using System.Text.Json;
using EsemprendedorApi.Application.Services.Interfaces;
using EsemprendedorApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace EsemprendedorApi.Pages.Dashboard.Cards
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly IImageStorageService _imageStorage;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            AppDbContext db, 
            IWebHostEnvironment env,
            IImageStorageService imageStorage,
            ILogger<IndexModel> logger)
        {
            _db = db;
            _env = env;
            _imageStorage = imageStorage;
            _logger = logger;
        }

        public List<CardView> Cards { get; set; } = new();
        public List<SectionOption> SectionsList { get; set; } = new();

        [BindProperty]
        public CardInput? Input { get; set; }

        public async Task OnGetAsync()
        {
            SectionsList = await _db.Sections
                .OrderBy(s => s.Title)
                .Select(s => new SectionOption { Id = s.Id, Title = s.Title })
                .ToListAsync();

            var cardsFromDb = await _db.Cards
                .Include(c => c.Section)
                .OrderBy(c => c.Name)
                .ToListAsync();

            if (cardsFromDb.Any())
            {
                Cards = cardsFromDb.Select(c => new CardView
                {
                    Id = c.Id,
                    SectionId = c.SectionId,
                    SectionTitle = c.Section?.Title,
                    Icon = c.Icon,
                    Chip = c.Chip,
                    Name = c.Name,
                    Service = c.Service,
                    Contact = c.Contact,
                    Featured = c.Featured,
                    BackgroundImage = c.BackgroundImage,
                    Keywords = c.Keywords
                }).ToList();
                return;
            }

            // Load mock data from wwwroot/mock/cards.json when DB is empty
            try
            {
                var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var mockPath = Path.Combine(webRoot, "mock", "cards.json");
                if (System.IO.File.Exists(mockPath))
                {
                    var json = await System.IO.File.ReadAllTextAsync(mockPath);
                    var mocks = JsonSerializer.Deserialize<List<MockCardDto>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<MockCardDto>();

                    Cards = mocks.Select(m =>
                    {
                        var sectionId = SectionsList.FirstOrDefault(s => string.Equals(s.Title, m.SectionTitle, StringComparison.OrdinalIgnoreCase))?.Id ?? 0;
                        return new CardView
                        {
                            Id = 0,
                            SectionId = sectionId,
                            SectionTitle = m.SectionTitle,
                            Icon = m.Icon ?? string.Empty,
                            Chip = m.Chip ?? string.Empty,
                            Name = m.Name ?? string.Empty,
                            Service = m.Service ?? string.Empty,
                            Contact = m.Contact ?? string.Empty,
                            Featured = m.Featured,
                            BackgroundImage = m.BackgroundImage,
                            Keywords = m.Keywords
                        };
                    }).ToList();
                }
            }
            catch
            {
                // swallow errors for mock loading - page will show empty list
            }
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (Input == null) return BadRequest();

            // Handle image upload if provided
            string? imageUrl = null;
            if (Input.ImageFile != null && Input.ImageFile.Length > 0)
            {
                try
                {
                    using var stream = Input.ImageFile.OpenReadStream();
                    imageUrl = await _imageStorage.UploadImageAsync(
                        stream,
                        Input.ImageFile.FileName,
                        Input.ImageFile.ContentType);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to upload image for card");
                    ModelState.AddModelError("Input.ImageFile", "Failed to upload image. Please try again.");
                    return Page();
                }
            }

            var card = new Domain.Entities.Card
            {
                SectionId = Input.SectionId,
                Icon = Input.Icon?.Trim() ?? string.Empty,
                Chip = Input.Chip?.Trim() ?? string.Empty,
                Name = Input.Name?.Trim() ?? string.Empty,
                Service = Input.Service?.Trim() ?? string.Empty,
                Contact = Input.Contact?.Trim() ?? string.Empty,
                Featured = Input.Featured,
                BackgroundImage = imageUrl ?? (string.IsNullOrWhiteSpace(Input.BackgroundImage) ? null : Input.BackgroundImage.Trim()),
                Keywords = string.IsNullOrWhiteSpace(Input.Keywords) ? null : Input.Keywords.Trim()
            };

            _db.Cards.Add(card);
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            if (Input == null) return BadRequest();

            var card = await _db.Cards.FindAsync(id);
            if (card == null) return NotFound();

            // Handle image upload if provided
            if (Input.ImageFile != null && Input.ImageFile.Length > 0)
            {
                try
                {
                    // Delete old image if exists
                    if (!string.IsNullOrWhiteSpace(card.BackgroundImage))
                    {
                        await _imageStorage.DeleteImageAsync(card.BackgroundImage);
                    }

                    // Upload new image
                    using var stream = Input.ImageFile.OpenReadStream();
                    card.BackgroundImage = await _imageStorage.UploadImageAsync(
                        stream,
                        Input.ImageFile.FileName,
                        Input.ImageFile.ContentType);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to upload image for card {CardId}", id);
                    ModelState.AddModelError("Input.ImageFile", "Failed to upload image. Please try again.");
                    return Page();
                }
            }
            else if (!string.IsNullOrWhiteSpace(Input.BackgroundImage))
            {
                // Keep or update URL manually if no file uploaded
                card.BackgroundImage = Input.BackgroundImage.Trim();
            }

            card.SectionId = Input.SectionId;
            card.Icon = Input.Icon?.Trim() ?? string.Empty;
            card.Chip = Input.Chip?.Trim() ?? string.Empty;
            card.Name = Input.Name?.Trim() ?? string.Empty;
            card.Service = Input.Service?.Trim() ?? string.Empty;
            card.Contact = Input.Contact?.Trim() ?? string.Empty;
            card.Featured = Input.Featured;
            card.Keywords = string.IsNullOrWhiteSpace(Input.Keywords) ? null : Input.Keywords.Trim();

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var card = await _db.Cards.FindAsync(id);
            if (card == null) return NotFound();

            // Delete associated image if exists
            if (!string.IsNullOrWhiteSpace(card.BackgroundImage))
            {
                try
                {
                    await _imageStorage.DeleteImageAsync(card.BackgroundImage);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to delete image for card {CardId}", id);
                    // Continue with card deletion even if image deletion fails
                }
            }

            _db.Cards.Remove(card);
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }

        // DTOs and records
        public record CardView
        {
            public int Id { get; init; }
            public int SectionId { get; init; }
            public string? SectionTitle { get; init; }
            public string Icon { get; init; } = string.Empty;
            public string Chip { get; init; } = string.Empty;
            public string Name { get; init; } = string.Empty;
            public string Service { get; init; } = string.Empty;
            public string Contact { get; init; } = string.Empty;
            public bool Featured { get; init; }
            public string? BackgroundImage { get; init; }
            public string? Keywords { get; init; }
        }

        public record SectionOption
        {
            public int Id { get; init; }
            public string? Title { get; init; }
        }

        public class CardInput
        {
            public int SectionId { get; set; }
            public string? Icon { get; set; }
            public string? Chip { get; set; }
            public string? Name { get; set; }
            public string? Service { get; set; }
            public string? Contact { get; set; }
            public bool Featured { get; set; }
            public string? BackgroundImage { get; set; }
            public string? Keywords { get; set; }
            public IFormFile? ImageFile { get; set; }
        }

        private class MockCardDto
        {
            public string? SectionTitle { get; set; }
            public string? Icon { get; set; }
            public string? Chip { get; set; }
            public string? Name { get; set; }
            public string? Service { get; set; }
            public string? Contact { get; set; }
            public bool Featured { get; set; }
            public string? BackgroundImage { get; set; }
            public string? Keywords { get; set; }
        }
    }
}
