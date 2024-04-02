﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using wcc.core.Infrastructure;

namespace wcc.core.data
{
    public interface IDataRepository
    {
        #region Tournament
        IList<Tournament> GetTournaments();
        Tournament? GetTournament(string tournamentId);
        bool SaveTournament(Tournament tournament);
        bool UpdateTournament(Tournament tournament);
        bool DeleteTournament(string tournamentId);
        #endregion Tournament

        #region Player
        IList<Player> GetPlayers();
        Player? GetPlayer(string playerId);
        bool SavePlayer(Player player);
        bool UpdatePlayer(Player player);
        bool DeletePlayer(string playerId);
        #endregion Player
    }
}
