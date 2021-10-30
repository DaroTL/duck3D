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
            int xPrim = vector.x * (90 / vector.z);
            int yPrim = vector.y * (90 / vector.z);
            Vector2 vector2 = new Vector2(xPrim, yPrim);
            return vector2;
        }
    }
}
