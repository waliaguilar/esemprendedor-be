using EsemprendedorApi.Application.DTOs;
using EsemprendedorApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EsemprendedorApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimpleCardsController : ControllerBase
{
    private readonly ISimpleCardService _service;

    public SimpleCardsController(ISimpleCardService service)
    {
        _service = service;
    }

    // GET: api/simplecards?sectionId=1
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SimpleCardDto>>> GetSimpleCards(
        [FromQuery] int? sectionId)
    {
        var simpleCards = await _service.GetAllAsync(sectionId);
        return Ok(simpleCards);
    }

    // GET: api/simplecards/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SimpleCardDto>> GetSimpleCard(int id)
    {
        var simpleCard = await _service.GetByIdAsync(id);
        return simpleCard is null ? NotFound() : Ok(simpleCard);
    }

    // POST: api/simplecards
    [HttpPost]
    public async Task<ActionResult<SimpleCardDto>> CreateSimpleCard(CreateSimpleCardDto dto)
    {
        var (result, error) = await _service.CreateAsync(dto);
        if (error is not null)
            return BadRequest(new { message = error });

        return CreatedAtAction(nameof(GetSimpleCard), new { id = result!.Id }, result);
    }

    // PUT: api/simplecards/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSimpleCard(int id, UpdateSimpleCardDto dto)
    {
        var error = await _service.UpdateAsync(id, dto);
        return error switch
        {
            null => NoContent(),
            "not_found" => NotFound(),
            _ => BadRequest(new { message = error })
        };
    }

    // DELETE: api/simplecards/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSimpleCard(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}