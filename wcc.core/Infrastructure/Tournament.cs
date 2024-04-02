using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.core.Infrastructure
{
    public class Tournament : Document
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public GameType GameType { get; set; }
        public List<string>? Participants { get; set; }
    }
}
