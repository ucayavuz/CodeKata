using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKata.Case2.Models
{
    public class OcrModel
    {
        public string Description { get; set; } = string.Empty;
        public VerticesModel Cordinates { get; set; } = new VerticesModel();

    }
}
