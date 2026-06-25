using EsemprendedorApi.Application.DTOs;
using EsemprendedorApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EsemprendedorApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionsController : ControllerBase
{
    private readonly ISectionService _service;

    public SectionsController(ISectionService service)
    {
        _service = service;
    }

    // GET: api/sections
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SectionDto>>> GetSections()
    {
        var sections = await _service.GetAllAsync();
        return Ok(sections);
    }

    // GET: api/sections/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SectionDto>> GetSection(int id)
    {
        var section = await _service.GetByIdAsync(id);
        return section is null ? NotFound() : Ok(section);
    }

    // GET: api/sections/by-slug/gastronomia
    [HttpGet("by-slug/{slug}")]
    public async Task<ActionResult<SectionDto>> GetSectionBySlug(string slug)
    {
        var section = await _service.GetBySlugAsync(slug);
        return section is null ? NotFound() : Ok(section);
    }

    // POST: api/sections
    [HttpPost]
    public async Task<ActionResult<SectionDto>> CreateSection(CreateSectionDto dto)
    {
        var (result, error) = await _service.CreateAsync(dto);
        if (error is not null)
            return Conflict(new { message = error });

        return CreatedAtAction(nameof(GetSection), new { id = result!.Id }, result);
    }

    // PUT: api/sections/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSection(int id, UpdateSectionDto dto)
    {
        var error = await _service.UpdateAsync(id, dto);
        return error switch
        {
            null => NoContent(),
            "not_found" => NotFound(),
            _ => Conflict(new { message = error })
        };
    }

    // DELETE: api/sections/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSection(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}