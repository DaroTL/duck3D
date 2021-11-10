using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{

    public class Point3D
    {
        public static int idCount = 0;
        private int id = 0;

        public Vector3 local;
        public Vector3 global;

        public Point3D(Vector3 global, double x, double y, double z, int _id = 0)
        {
            this.local = new Vector3(x,y,z);
            this.global = new Vector3(x + global.x, y + global.y, z + global.z);
            if (_id == 0) id = idCount;
            else id = _id;
            idCount++;
        }

        public int getid()
        {
            return id;
        }

        public Vector2 convertVectorTo2D()
        {

            Vector3 a = global;
            Vector3 c = new Vector3(0, 0, -60);
            Vector3 o = new Vector3(0, 0, 0);
            Vector3 e = new Vector3(c.x, c.y, c.z + 20);
            Matrix d = new Matrix(new double[3, 1]);
            Matrix m1 = new Matrix(new double[,] { { 1, 0, 0 }, { 0, Math.Cos(o.x), Math.Sin(o.x) }, { 0, -Math.Sin(o.x), Math.Cos(o.x) } });
            Matrix m2 = new Matrix(new double[,] { { Math.Cos(o.y), 0, -Math.Sin(o.y) }, { 0, 1, 0 }, { Math.Sin(o.y), 0, Math.Cos(o.y) } });
            Matrix m3 = new Matrix(new double[,] { { Math.Cos(o.z), Math.Sin(o.z), 0 }, { -Math.Sin(o.z), Math.Cos(o.z), 0 }, { 0, 0, 1 } });
            Matrix aMatrix = new Matrix(new double[,] { { a.x }, { a.y }, { a.z } });
            Matrix cMatrix = new Matrix(new double[,] { { c.x }, { c.y }, { c.z } });
            Matrix aminusc = aMatrix.subtract(cMatrix);
            Matrix temp;
            temp = m1.multiply(m2);
            temp = temp.multiply(m3);
            d = temp.multiply(aminusc);
            Vector3 dVector = new Vector3(d.tablica[0, 0], d.tablica[1, 0], d.tablica[2, 0]);
            Vector2 vect = new Vector2((int)((e.z / dVector.z * dVector.x) + e.x), (int)((e.z / dVector.z * dVector.y) + e.y));

            return vect;

        }

        public void setGlobal(Vector3 x)
        {
            global.x += x.x;
            global.y += x.y;
            global.z += x.z;
        }
    }
}
