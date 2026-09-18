using Microsoft.AspNetCore.Mvc;
using narutodleAPI.dtos;
using narutodleAPI.services;
using System.Threading.Tasks;

namespace narutodleAPI.controllers;

[ApiController]
[Route("api/[controller]")] 
public class GameController : ControllerBase
{
    private readonly GameService _gameService;

    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("guess/{guessedName}")]
    public async Task<ActionResult<GuessResultDto>> MakeGuess(string guessedName)
    {
        var result = await _gameService.ProcessGuess(guessedName);

        if (result == null)
        {
            return NotFound($"Ninja '{guessedName}' not found in the database. Try a valid name!");
        }

        return Ok(result);
    }

    [HttpGet("names")]
    public async Task<ActionResult<IEnumerable<NinjaDropdownDto>>> GetCharacterNames()
    {
        var list = await _gameService.GetNinjaDropdownListAsync();
        return Ok(list);
    }
}