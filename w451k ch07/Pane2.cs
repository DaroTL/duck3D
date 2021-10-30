using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{
    public class Pane2
    {
        public Line2 l1;
        public Line2 l2;
        public Line2 l3;
        public Line2 l4;

        public Pane2(Line2 l1, Line2 l2, Line2 l3, Line2 l4)
        {
            this.l1 = l1;
            this.l2 = l2;
            this.l3 = l3;
            this.l4 = l4;
        }

        public static Pane2 convertPane3(Pane3 pane3)
        {
            Pane2 newPane = new Pane2(Line2.convertLine3(pane3.l1), Line2.convertLine3(pane3.l2), Line2.convertLine3(pane3.l3), Line2.convertLine3(pane3.l4));
            return newPane;
        }
    }
}
