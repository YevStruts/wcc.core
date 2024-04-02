using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Web;
using wcc.core.kernel.Models;
using wcc.core.kernel.RequestHandlers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace wcc.core.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        protected readonly ILogger<TournamentController>? _logger;
        protected readonly IMediator? _mediator;

        public TournamentController(ILogger<TournamentController>? logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<TournamentModel>> Get()
        {
            return await _mediator.Send(new GetTournamentsQuery());
        }

        [HttpGet("{id}")]
        public async Task<TournamentModel> Get(string id)
        {
            string tournamentId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new GetTournamentQuery(tournamentId));
        }

        [HttpPost]
        public async Task<bool> Post(TournamentModel tournament)
        {
            return await _mediator.Send(new SaveOrUpdateTournamentQuery(tournament));
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            string tournamentId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new DeleteTournamentQuery(tournamentId));
        }
    }
}
