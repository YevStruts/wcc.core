using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace wcc.core
{
    public abstract class Document
    {
        public Document()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public DateTime CreatedAt { get; protected set; }
    }
}
