using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{
    public class Line2
    {
        public Vector2 v1;
        public Vector2 v2;

        public Line2(Vector2 v1, Vector2 v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
        public Line2(float x1, float y1, float x2, float y2)
        {
            this.v1 = new Vector2(x1, y1);
            this.v2 = new Vector2(x2, y2);
        }
        

        
    }
}
