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
    public class GetGamesQuery : IRequest<IList<GameModel>>
    {
        public string TournamentId { get; private set; }
        public int Page { get; private set; }
        public int Count { get; private set; }

        public GetGamesQuery(string tournamentId, int page, int count)
        {
            TournamentId = tournamentId;
            Page = page <= 0 ? 1 : page;
            Count = count;
        }
    }

    public class GetGameQuery : IRequest<GameModel>
    {
        public string GameId { get; set; }

        public GetGameQuery(string gameId)
        {
            this.GameId = gameId;
        }
    }

    public class SaveOrUpdateGameQuery : IRequest<bool>
    {
        public GameModel Game { get; set; }

        public SaveOrUpdateGameQuery(GameModel game)
        {
            this.Game = game;
        }
    }

    public class DeleteGameQuery : IRequest<bool>
    {
        public string GameId { get; set; }

        public DeleteGameQuery(string gameId)
        {
            this.GameId = gameId;
        }
    }

    public class GameHandler : IRequestHandler<GetGamesQuery, IList<GameModel>>,
        IRequestHandler<GetGameQuery, GameModel>,
        IRequestHandler<SaveOrUpdateGameQuery, bool>,
        IRequestHandler<DeleteGameQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public GameHandler(IDataRepository db)
        {
            _db = db;
        }

        public async Task<IList<GameModel>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            var games = string.IsNullOrEmpty(request.TournamentId) ?
                _db.GetGames(request.Page, request.Count) :
                _db.GetGamesForTournament(request.TournamentId, request.Page, request.Count);
            return _mapper.Map<List<GameModel>>(games);
        }

        public async Task<GameModel> Handle(GetGameQuery request, CancellationToken cancellationToken)
        {
            var game = _db.GetGame(request.GameId);
            return _mapper.Map<GameModel>(game);
        }
        
        public async Task<bool> Handle(SaveOrUpdateGameQuery request, CancellationToken cancellationToken)
        {
            Game game = _mapper.Map<Game>(request.Game);
            if (string.IsNullOrEmpty(game.Id))
            {
                return _db.SaveGame(game);
            }
            return _db.UpdateGame(game);
        }

        public async Task<bool> Handle(DeleteGameQuery request, CancellationToken cancellationToken)
        {
            return _db.DeleteGame(request.GameId);
        }
    }
}
