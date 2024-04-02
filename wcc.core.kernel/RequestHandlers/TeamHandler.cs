using AutoMapper;
using MediatR;
using Nito.Disposables;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.core.data;
using wcc.core.Infrastructure;
using wcc.core.kernel.Helpers;
using wcc.core.kernel.Models;

namespace wcc.core.kernel.RequestHandlers
{
    public class GetTeamsQuery : IRequest<IList<TeamModel>>
    {
    }

    public class GetTeamQuery : IRequest<TeamModel>
    {
        public string TeamId { get; set; }

        public GetTeamQuery(string teamId)
        {
            this.TeamId = teamId;
        }
    }

    public class SaveOrUpdateTeamQuery : IRequest<bool>
    {
        public TeamModel Team { get; set; }

        public SaveOrUpdateTeamQuery(TeamModel team)
        {
            this.Team = team;
        }
    }

    public class DeleteTeamQuery : IRequest<bool>
    {
        public string TeamId { get; set; }

        public DeleteTeamQuery(string teamId)
        {
            this.TeamId = teamId;
        }
    }

    public class TeamHandler : IRequestHandler<GetTeamsQuery, IList<TeamModel>>,
        IRequestHandler<GetTeamQuery, TeamModel>,
        IRequestHandler<SaveOrUpdateTeamQuery, bool>,
        IRequestHandler<DeleteTeamQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public TeamHandler(IDataRepository db)
        {
            _db = db;
        }

        public async Task<IList<TeamModel>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = _db.GetTeams();
            return _mapper.Map<List<TeamModel>>(teams);
        }

        public async Task<TeamModel> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            var team = _db.GetTeam(request.TeamId);
            return _mapper.Map<TeamModel>(team);
        }
        
        public async Task<bool> Handle(SaveOrUpdateTeamQuery request, CancellationToken cancellationToken)
        {
            Team team = _mapper.Map<Team>(request.Team);
            if (string.IsNullOrEmpty(team.Id))
            {
                return _db.SaveTeam(team);
            }
            return _db.UpdateTeam(team);
        }

        public async Task<bool> Handle(DeleteTeamQuery request, CancellationToken cancellationToken)
        {
            return _db.DeleteTeam(request.TeamId);
        }
    }
}
