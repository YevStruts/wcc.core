using Newtonsoft.Json.Linq;
using Raven.Client.Documents.Session;
using Sparrow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.core.Infrastructure;
using static System.Collections.Specialized.BitVector32;
using static System.Formats.Asn1.AsnWriter;

namespace wcc.core.data
{
    public class DataRepository : IDataRepository
    {
        #region Tournament
        public IList<Tournament> GetTournaments()
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Query<Tournament>().ToList();
            }
        }

        public Tournament? GetTournament(string tournamentId)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Query<Tournament>().FirstOrDefault(x => x.Id == tournamentId);
            }
        }

        public bool SaveTournament(Tournament tournament)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(new Tournament()
                {
                    Name = tournament.Name,
                    Description = tournament.Description,
                    ImageUrl = tournament.ImageUrl,
                    GameType = tournament.GameType,
                    Participants = tournament.Participants
                });
                session.SaveChanges();
            }
            return true;
        }
        public bool UpdateTournament(Tournament tournament)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                var tournamentDto = session.Query<Tournament>().FirstOrDefault(x => x.Id == tournament.Id);
                if (tournamentDto == null) return false;

                tournamentDto.Name = tournament.Name;
                tournamentDto.Description = tournament.Description;
                tournamentDto.ImageUrl = tournament.ImageUrl;
                tournamentDto.GameType = tournament.GameType;
                tournamentDto.Participants = tournament.Participants;

                session.SaveChanges();
            }
            return true;
        }
        public bool DeleteTournament(string tournamentId)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Delete(tournamentId);
                session.SaveChanges();
            }
            return true;
        }
        #endregion Tournament

        #region Player
        public IList<Player> GetPlayers()
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Query<Player>().ToList();
            }
        }

        public Player? GetPlayer(string playerId)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                return session.Query<Player>().FirstOrDefault(x => x.Id == playerId);
            }
        }

        public bool SavePlayer(Player player)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(new Player()
                {
                    Name = player.Name,
                    IsActive = player.IsActive,
                    Token = player.Token
                });
                session.SaveChanges();
            }
            return true;
        }
        public bool UpdatePlayer(Player player)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                var playerDto = session.Query<Player>().FirstOrDefault(x => x.Id == player.Id);
                if (playerDto == null) return false;

                playerDto.Name = player.Name;
                playerDto.IsActive = player.IsActive;
                playerDto.Token = player.Token;

                session.SaveChanges();
            }
            return true;
        }
        public bool DeletePlayer(string playerId)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Delete(playerId);
                session.SaveChanges();
            }
            return true;
        }
        #endregion Player
    }
}
