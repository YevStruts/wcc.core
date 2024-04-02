using AutoMapper;
using wcc.core.Infrastructure;
using wcc.core.kernel.Models;

namespace wcc.core.kernel.Helpers
{
    public sealed class MapperHelper
    {
        private static IMapper? instance = null;

        private MapperHelper()
        {
        }

        public static IMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Game, GameModel>().ReverseMap();
                        cfg.CreateMap<Tournament, TournamentModel>().ReverseMap();
                        cfg.CreateMap<Player, PlayerModel>().ReverseMap();
                        cfg.CreateMap<Team, TeamModel>().ReverseMap();
                    });

                    instance = new Mapper(config);
                }
                return instance;
            }
        }
    }
}
