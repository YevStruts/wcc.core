﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.core.kernel.Models.Results
{
    public class SaveOrUpdateResult<T> : Result
    {
        public T Value { get; set; }
    }
}
