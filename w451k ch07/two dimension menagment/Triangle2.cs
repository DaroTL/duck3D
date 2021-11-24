using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{

    public class Triangle2
    {
        public Line2 l1;
        public Line2 l2;
        public Line2 l3;



        public Vector2 v0;
        public Vector2 v1;
        public Vector2 v2;

        public Triangle2(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            this.v0 = p1;
            this.v1 = p2;
            this.v2 = p3;
            this.l1 = new Line2(v0, v1);
            this.l2 = new Line2(v1, v2);
            this.l3 = new Line2(v2, v0);
        }
    }
}
