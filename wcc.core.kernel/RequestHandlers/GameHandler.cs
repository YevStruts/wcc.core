using AutoMapper;
using MediatR;
using wcc.core.data;
using wcc.core.Infrastructure;
using wcc.core.kernel.Helpers;
using wcc.core.kernel.Models;

namespace wcc.core.kernel.RequestHandlers
{
    public class SaveGameQuery : IRequest<bool>
    {
        public GameModel Game { get; set; }

        public SaveGameQuery(GameModel game)
        {
            this.Game = game;
        }
    }

    public class GameHandler : IRequestHandler<SaveGameQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public GameHandler(IDataRepository db)
        {
            _db = db;
        }

        public async Task<bool> Handle(SaveGameQuery request, CancellationToken cancellationToken)
        {
            return false;
        }
    }
}
