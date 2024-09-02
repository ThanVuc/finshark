using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? Company {get; set;} = null;
        public string? Industry {get; set;} = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageCurrent {get; set;} = 1;
        public int PageSize {get; set;} = 2;
    }
}