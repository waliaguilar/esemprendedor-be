using EsemprendedorApi.Application.DTOs;
using EsemprendedorApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EsemprendedorApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly ICardService _service;

    public CardsController(ICardService service)
    {
        _service = service;
    }

    // GET: api/cards?sectionId=1&featured=true
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CardDto>>> GetCards(
        [FromQuery] int? sectionId,
        [FromQuery] bool? featured)
    {
        var cards = await _service.GetAllAsync(sectionId, featured);
        return Ok(cards);
    }

    // GET: api/cards/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CardDto>> GetCard(int id)
    {
        var card = await _service.GetByIdAsync(id);
        return card is null ? NotFound() : Ok(card);
    }

    // POST: api/cards
    [HttpPost]
    public async Task<ActionResult<CardDto>> CreateCard(CreateCardDto dto)
    {
        var (result, error) = await _service.CreateAsync(dto);
        if (error is not null)
            return BadRequest(new { message = error });

        return CreatedAtAction(nameof(GetCard), new { id = result!.Id }, result);
    }

    // PUT: api/cards/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCard(int id, UpdateCardDto dto)
    {
        var error = await _service.UpdateAsync(id, dto);
        return error switch
        {
            null => NoContent(),
            "not_found" => NotFound(),
            _ => BadRequest(new { message = error })
        };
    }

    // DELETE: api/cards/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCard(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}