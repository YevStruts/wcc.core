using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.core.Infrastructure
{
    public class Player : Document
    {
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public string? Token { get; set; }
    }
}
