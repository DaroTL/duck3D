using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duck
{
    public class Pane2
    {
        public Vector2 v1;
        public Vector2 v2;
        public Vector2 v3;
        public Vector2 v4;
        public List<Line2> lines = new List<Line2>();

        public Pane2(Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v4)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
            lines.Add(new Line2(v1, v2));
            lines.Add(new Line2(v2, v3));
            lines.Add(new Line2(v3, v4));
            lines.Add(new Line2(v4, v1));
        }

    }
}
