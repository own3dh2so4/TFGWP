using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Charts
{
    class PieData
    {
        public string Title { get; set; }
        public double Value { get; set; }

        public PieData() { }
        public PieData(string t, double v)
        {
            Title = t;
            Value = v;
        }
    }
}
