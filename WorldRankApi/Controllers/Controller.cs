using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WorldRank.Application.Interfaces;

namespace WorldRankApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller(IPlayerRepository playerRepository) : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository = playerRepository;

        [HttpGet("search_player/{playerId:guid}")]
        public async Task<IActionResult> FetchPlayer([FromRoute]int playerId)
        {
            try
            {
                var result = _playerRepository.FindPlayer(playerId);
                if(result == null) return NotFound();

                return Ok(result); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> FetchAllPlayers()
        {

            try
            {
                var players = _playerRepository.GetAllPlayers();

                if (players == null) return NotFound();

                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
