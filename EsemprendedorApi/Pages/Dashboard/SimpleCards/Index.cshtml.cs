using EsemprendedorApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EsemprendedorApi.Pages.Dashboard.SimpleCards
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db) => _db = db;

        public List<SimpleCardView> SimpleCards { get; set; } = new();
        public List<SectionOption> SectionsList { get; set; } = new();

        [BindProperty]
        public SimpleCardInput? Input { get; set; }

        public async Task OnGetAsync()
        {
            var scs = await _db.SimpleCards
                .Include(sc => sc.Section)
                .OrderBy(sc => sc.Name)
                .ToListAsync();

            SimpleCards = scs.Select(sc => new SimpleCardView
            {
                Id = sc.Id,
                SectionId = sc.SectionId,
                SectionTitle = sc.Section.Title,
                Name = sc.Name,
                Service = sc.Service,
                Contact = sc.Contact,
                Keywords = sc.Keywords
            }).ToList();

            SectionsList = await _db.Sections
                .OrderBy(s => s.Title)
                .Select(s => new SectionOption { Id = s.Id, Title = s.Title })
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (Input == null) return BadRequest();

            var sc = new Domain.Entities.SimpleCard
            {
                SectionId = Input.SectionId,
                Name = Input.Name?.Trim() ?? string.Empty,
                Service = Input.Service?.Trim() ?? string.Empty,
                Contact = Input.Contact?.Trim() ?? string.Empty,
                Keywords = string.IsNullOrWhiteSpace(Input.Keywords) ? null : Input.Keywords.Trim()
            };

            _db.SimpleCards.Add(sc);
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            if (Input == null) return BadRequest();

            var sc = await _db.SimpleCards.FindAsync(id);
            if (sc == null) return NotFound();

            sc.SectionId = Input.SectionId;
            sc.Name = Input.Name?.Trim() ?? string.Empty;
            sc.Service = Input.Service?.Trim() ?? string.Empty;
            sc.Contact = Input.Contact?.Trim() ?? string.Empty;
            sc.Keywords = string.IsNullOrWhiteSpace(Input.Keywords) ? null : Input.Keywords.Trim();

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var sc = await _db.SimpleCards.FindAsync(id);
            if (sc == null) return NotFound();

            _db.SimpleCards.Remove(sc);
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }

        public record SimpleCardView
        {
            public int Id { get; init; }
            public int SectionId { get; init; }
            public string? SectionTitle { get; init; }
            public string Name { get; init; } = string.Empty;
            public string Service { get; init; } = string.Empty;
            public string Contact { get; init; } = string.Empty;
            public string? Keywords { get; init; }
        }

        public record SectionOption
        {
            public int Id { get; init; }
            public string? Title { get; init; }
        }

        public class SimpleCardInput
        {
            public int SectionId { get; set; }
            public string? Name { get; set; }
            public string? Service { get; set; }
            public string? Contact { get; set; }
            public string? Keywords { get; set; }
        }
    }
}
