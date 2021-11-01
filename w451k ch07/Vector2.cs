using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{
    public class Vector2
    {
        public int x;
        public int y;
        
        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 convertVector3(Vector3 vector)
        {
            int pov = 90;
            int xPrim = vector.x * (pov / vector.z);
            int yPrim = vector.y * (pov / vector.z) + 500;
            Vector2 vector2 = new Vector2(xPrim, yPrim);
            return vector2;
        }
    }
}
