using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using wcc.core.kernel.Models;
using wcc.core.kernel.Models.Results;
using wcc.core.kernel.RequestHandlers;

namespace wcc.core.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        protected readonly ILogger<GameController>? _logger;
        protected readonly IMediator? _mediator;

        public GameController(ILogger<GameController>? logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GameModel>> Get(string? tournamentId = null, int page = 1, int count = 20)
        {
            return await _mediator.Send(new GetGamesQuery(tournamentId, page, count));
        }

        [HttpGet("{id}")]
        public async Task<GameModel> Get(string id)
        {
            string gameId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new GetGameQuery(gameId));
        }


        [HttpGet, Route("player/{id}")]
        public async Task<IEnumerable<GameModel>> GetGamesForPlayer(string id)
        {
            string playerId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new GetGamesForPlayerQuery(playerId));
        }

        [HttpPost]
        public async Task<SaveOrUpdateResult<GameModel>> Post(GameModel game)
        {
            return await _mediator.Send(new SaveOrUpdateGameQuery(game));
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            string gameId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new DeleteGameQuery(gameId));
        }

        [HttpGet, Route("Count")]
        public async Task<int> Count(string? tournamentId = null)
        {
            return await _mediator.Send(new GetGamesCountQuery(tournamentId));
        }
    }
}
