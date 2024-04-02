using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using wcc.core.kernel.Models;
using wcc.core.kernel.RequestHandlers;

namespace wcc.core.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        protected readonly ILogger<TeamController>? _logger;
        protected readonly IMediator? _mediator;

        public TeamController(ILogger<TeamController>? logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<TeamModel>> Get()
        {
            return await _mediator.Send(new GetTeamsQuery());
        }

        [HttpGet("{id}")]
        public async Task<TeamModel> Get(string id)
        {
            string teamId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new GetTeamQuery(teamId));
        }

        [HttpPost]
        public async Task<bool> Post(TeamModel team)
        {
            return await _mediator.Send(new SaveOrUpdateTeamQuery(team));
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            string teamId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new DeleteTeamQuery(teamId));
        }
    }
}
