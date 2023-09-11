using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKata.Case2.Models
{
    public class ResultModel
    {
        public List<RowModel> SameItems { get; set; } = new List<RowModel>();
        public int Index { get; set; }
    }
}
