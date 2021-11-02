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
            
            Vector3 a = vector;
            Vector3 c = new Vector3(0, 0, -60);
            Vector3 o = new Vector3(0, 0, 0);
            Vector3 e = new Vector3(c.x, c.y, c.z + 20);
            Matrix d = new Matrix(new double[3, 1]);
            Matrix m1 = new Matrix(new double[,] { { 1, 0, 0 }, { 0, Math.Cos(o.x), Math.Sin(o.x) }, { 0, -Math.Sin(o.x), Math.Cos(o.x) } });
            Matrix m2 = new Matrix(new double[,] { { Math.Cos(o.y), 0, -Math.Sin(o.y) }, { 0, 1, 0 }, { Math.Sin(o.y), 0, Math.Cos(o.y) } });
            Matrix m3 = new Matrix(new double[,] { { Math.Cos(o.z), Math.Sin(o.z), 0 }, { -Math.Sin(o.z), Math.Cos(o.z), 0 }, { 0, 0, 1 } });
            Matrix aMatrix = new Matrix(new double[,] { { a.x }, { a.y }, { a.z } } );
            Matrix cMatrix = new Matrix(new double[,] { { c.x }, { c.y }, { c.z } } );
            Matrix aminusc = aMatrix.subtract(cMatrix);
            Matrix temp;
            temp = m1.multiply(m2);
            temp = temp.multiply(m3);
            d = temp.multiply(aminusc);
            Vector3 dVector = new Vector3(d.tablica[0, 0], d.tablica[1,0], d.tablica[2, 0]);
            Vector2 vect = new Vector2((int)((e.z / dVector.z * dVector.x) + e.x), (int)((e.z / dVector.z * dVector.y) + e.y));
            
            return vect;
            /*
            int pov = 90;
            int xPrim = vector.x * (pov / vector.z);
            int yPrim = vector.y * (pov / vector.z) + 500;
            Vector2 vector2 = new Vector2(xPrim, yPrim);
            return vector2;
            */
            
        }
    }
}
