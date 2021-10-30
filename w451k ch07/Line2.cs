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
        public Line2(int x1, int y1, int x2, int y2)
        {
            this.v1 = new Vector2(x1, y1);
            this.v2 = new Vector2(x2, y2);
        }
        
        public static Line2 convertLine3(Line3 line3)
        {
            Line2 newLine = new Line2(Vector2.convertVector3(line3.v1), Vector2.convertVector3(line3.v2));
            return newLine;
        }
        
    }
}
