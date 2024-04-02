using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using wcc.core.kernel.Models;
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
        public async Task<IEnumerable<GameModel>> Get()
        {
            return await _mediator.Send(new GetGamesQuery());
        }

        [HttpGet("{id}")]
        public async Task<GameModel> Get(string id)
        {
            string gameId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new GetGameQuery(gameId));
        }

        [HttpPost]
        public async Task<bool> Post(GameModel game)
        {
            return await _mediator.Send(new SaveOrUpdateGameQuery(game));
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            string gameId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new DeleteGameQuery(gameId));
        }
    }
}
