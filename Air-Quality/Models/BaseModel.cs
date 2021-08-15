using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_Quality.Models
{
    public class BaseModel
    {
        public int Page { get; set; }

        public int Limit { get; set; }

        public int Found { get; set; }
    }
}

