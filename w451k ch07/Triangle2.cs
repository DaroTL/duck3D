using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{
 
    class Triangle2
    {
        public Line2 l1;
        public Line2 l2;
        public Line2 l3;

        public Triangle2(Line2 l1, Line2 l2)
        {
            this.l1 = l1;
            this.l2 = l2;
            this.l3 = new Line2(l1.v1, l2.v2);
        }
    }
}
