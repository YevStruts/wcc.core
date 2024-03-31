using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.core.Infrastructure
{
    public class Team
    {
        public long TeamId { get; set; }
        public string Name { get; set; }
        public List<long> PlayerIds { get; set; }
        public long TournamentId { get; set; }
    }
}
