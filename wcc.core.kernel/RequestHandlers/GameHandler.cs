﻿using AutoMapper;
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
using wcc.core.kernel.Models.Results;

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

    public class GetGamesForPlayerQuery : IRequest<IList<GameModel>>
    {
        public string PlayerId { get; set; }

        public GetGamesForPlayerQuery(string playerId)
        {
            this.PlayerId = playerId;
        }
    }

    public class SaveOrUpdateGameQuery : IRequest<SaveOrUpdateResult<GameModel>>
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

    public class GetGamesCountQuery :IRequest<int>
    {
        public string TournamentId { get; private set; }
        public GetGamesCountQuery(string tournamentId)
        {
            TournamentId = tournamentId;
        }
    }

    public class GameHandler : IRequestHandler<GetGamesQuery, IList<GameModel>>,
        IRequestHandler<GetGameQuery, GameModel>,
        IRequestHandler<GetGamesForPlayerQuery, IList<GameModel>>,
        IRequestHandler<SaveOrUpdateGameQuery, SaveOrUpdateResult<GameModel>>,
        IRequestHandler<DeleteGameQuery, bool>,
        IRequestHandler<GetGamesCountQuery, int>
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

        public async Task<IList<GameModel>> Handle(GetGamesForPlayerQuery request, CancellationToken cancellationToken)
        {
            var games = _db.GetGamesForPlayer(request.PlayerId);
            return _mapper.Map<IList<GameModel>>(games);
        }

        public async Task<SaveOrUpdateResult<GameModel>> Handle(SaveOrUpdateGameQuery request, CancellationToken cancellationToken)
        {
            bool success = false;
            GameModel value = request.Game;

            Game game = _mapper.Map<Game>(request.Game);
            if (string.IsNullOrEmpty(game.Id))
            {
                success = _db.SaveGame(game);
                if (success)
                {
                    value = _mapper.Map<GameModel>(game);
                }
            }
            else
            {
                success = _db.UpdateGame(game);
                if (success)
                {
                    value = _mapper.Map<GameModel>(game);
                }
            }
            return new SaveOrUpdateResult<GameModel>() { Success = success, Value = value };
        }

        public async Task<bool> Handle(DeleteGameQuery request, CancellationToken cancellationToken)
        {
            return _db.DeleteGame(request.GameId);
        }

        public async Task<int> Handle(GetGamesCountQuery request, CancellationToken cancellationToken)
        {
            return _db.GetGamesCountForTournament(request.TournamentId);
        }
    }
}
