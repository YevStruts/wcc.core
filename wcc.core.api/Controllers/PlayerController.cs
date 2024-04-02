using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using wcc.core.kernel.Models;
using wcc.core.kernel.RequestHandlers;

namespace wcc.core.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        protected readonly ILogger<PlayerController>? _logger;
        protected readonly IMediator? _mediator;

        public PlayerController(ILogger<PlayerController>? logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<PlayerModel>> Get()
        {
            return await _mediator.Send(new GetPlayersQuery());
        }

        [HttpGet("{id}")]
        public async Task<PlayerModel> Get(string id)
        {
            string playerId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new GetPlayerQuery(playerId));
        }

        [HttpPost]
        public async Task<bool> Post(PlayerModel player)
        {
            return await _mediator.Send(new SaveOrUpdatePlayerQuery(player));
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            string playerId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new DeletePlayerQuery(playerId));
        }
    }
}
