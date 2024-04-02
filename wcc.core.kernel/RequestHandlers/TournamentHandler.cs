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
    public class GetTournamentsQuery : IRequest<IList<TournamentModel>>
    {
    }

    public class GetTournamentQuery : IRequest<TournamentModel>
    {
        public string TournamentId { get; set; }

        public GetTournamentQuery(string tournamentId)
        {
            this.TournamentId = tournamentId;
        }
    }

    public class SaveOrUpdateTournamentQuery : IRequest<bool>
    {
        public TournamentModel Tournament { get; set; }

        public SaveOrUpdateTournamentQuery(TournamentModel tournament)
        {
            this.Tournament = tournament;
        }
    }

    public class DeleteTournamentQuery : IRequest<bool>
    {
        public string TournamentId { get; set; }

        public DeleteTournamentQuery(string tournamentId)
        {
            this.TournamentId = tournamentId;
        }
    }

    public class TournamentHandler : IRequestHandler<GetTournamentsQuery, IList<TournamentModel>>,
        IRequestHandler<GetTournamentQuery, TournamentModel>,
        IRequestHandler<SaveOrUpdateTournamentQuery, bool>,
        IRequestHandler<DeleteTournamentQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public TournamentHandler(IDataRepository db)
        {
            _db = db;
        }

        public async Task<IList<TournamentModel>> Handle(GetTournamentsQuery request, CancellationToken cancellationToken)
        {
            var tournaments = _db.GetTournaments();
            return _mapper.Map<List<TournamentModel>>(tournaments);
        }

        public async Task<TournamentModel> Handle(GetTournamentQuery request, CancellationToken cancellationToken)
        {
            var tournament = _db.GetTournament(request.TournamentId);
            return _mapper.Map<TournamentModel>(tournament);
        }
        
        public async Task<bool> Handle(SaveOrUpdateTournamentQuery request, CancellationToken cancellationToken)
        {
            Tournament tournament = _mapper.Map<Tournament>(request.Tournament);
            if (string.IsNullOrEmpty(tournament.Id))
            {
                return _db.SaveTournament(tournament);
            }
            return _db.UpdateTournament(tournament);
        }

        public async Task<bool> Handle(DeleteTournamentQuery request, CancellationToken cancellationToken)
        {
            return _db.DeleteTournament(request.TournamentId);
        }
    }
}
