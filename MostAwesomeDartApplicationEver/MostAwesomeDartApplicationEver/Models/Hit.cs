using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostAwesomeDartApplicationEver.Models
{
    public class Hit : IFormattable
    {
        public int? Id { get; set; }
        public HitArea HitArea { get; set; }
        public int Score { get; set; }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return "(" + HitArea + ", " + Score + ")";
        }
    }
}
