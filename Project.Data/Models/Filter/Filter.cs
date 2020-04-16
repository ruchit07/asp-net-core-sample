using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Data.Models
{
    public class Filter
    {
        public int PageSize { get; set; }

        public int PageNum { get; set; }

        public string Query { get; set; }
    }
}
