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
    public class GetPlayersQuery : IRequest<IList<PlayerModel>>
    {
    }

    public class GetPlayerQuery : IRequest<PlayerModel>
    {
        public string PlayerId { get; set; }

        public GetPlayerQuery(string playerId)
        {
            this.PlayerId = playerId;
        }
    }

    public class SaveOrUpdatePlayerQuery : IRequest<bool>
    {
        public PlayerModel Player { get; set; }

        public SaveOrUpdatePlayerQuery(PlayerModel player)
        {
            this.Player = player;
        }
    }

    public class DeletePlayerQuery : IRequest<bool>
    {
        public string PlayerId { get; set; }

        public DeletePlayerQuery(string playerId)
        {
            this.PlayerId = playerId;
        }
    }

    public class PlayerHandler : IRequestHandler<GetPlayersQuery, IList<PlayerModel>>,
        IRequestHandler<GetPlayerQuery, PlayerModel>,
        IRequestHandler<SaveOrUpdatePlayerQuery, bool>,
        IRequestHandler<DeletePlayerQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public PlayerHandler(IDataRepository db)
        {
            _db = db;
        }

        public async Task<IList<PlayerModel>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            var players = _db.GetPlayers();
            return _mapper.Map<List<PlayerModel>>(players);
        }

        public async Task<PlayerModel> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {
            var player = _db.GetPlayer(request.PlayerId);
            return _mapper.Map<PlayerModel>(player);
        }
        
        public async Task<bool> Handle(SaveOrUpdatePlayerQuery request, CancellationToken cancellationToken)
        {
            Player player = _mapper.Map<Player>(request.Player);
            if (string.IsNullOrEmpty(player.Id))
            {
                return _db.SavePlayer(player);
            }
            return _db.UpdatePlayer(player);
        }

        public async Task<bool> Handle(DeletePlayerQuery request, CancellationToken cancellationToken)
        {
            return _db.DeletePlayer(request.PlayerId);
        }
    }
}
