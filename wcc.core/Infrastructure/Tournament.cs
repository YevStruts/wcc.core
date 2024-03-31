using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.core.Infrastructure
{
    public class Tournament
    {
        public long TournamentId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public GameType GameType { get; set; }
        public List<long>? Participants { get; set; }
    }
}
