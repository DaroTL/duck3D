using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{
    public class Vector3
    {
        public double x;
        public double y;
        public double z;
        static double angle = 0;

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void rotate()
        {
            Matrix matrixVector = new Matrix(new double[3, 1] { { x }, { y }, { z } });
            Matrix rotation_z = new Matrix(new double[,] { { Math.Cos(angle), -Math.Sin(angle), 0 }, { Math.Sin(angle), Math.Cos(angle), 0 }, { 0, 0, 1 } });
            Matrix rotation_y = new Matrix(new double[,] { { Math.Cos(angle), 0, Math.Sin(angle) }, { 0, 1, 0 }, { -Math.Sin(angle), 0, Math.Cos(angle) } });
            Matrix rotation_x = new Matrix(new double[,] { { 1, 0, 0 }, { 0, Math.Cos(angle), -Math.Sin(angle) }, { 0, Math.Sin(angle), Math.Cos(angle) } });
            Matrix temp = rotation_z.multiply(matrixVector);
            temp = rotation_y.multiply(temp);
            temp = rotation_x.multiply(temp);
            x = temp.tablica[0, 0];
            y = temp.tablica[1, 0];
            z = temp.tablica[2, 0];
            angle += 0.01;
        }

        public void smash()
        {
            Matrix matrixVector = new Matrix(new double[3, 1] { { x }, { y }, { z } });
            Matrix rotation_z = new Matrix(new double[,] { { Math.Cos(angle), -Math.Sin(angle), 0 }, { Math.Sin(angle), Math.Cos(angle), 0 }, { 0, 0, 1 } });
            Matrix rotation_y = new Matrix(new double[,] { { Math.Cos(angle), 0, Math.Sin(angle) }, { 0, 1, 0 }, { -Math.Sin(angle), 0, Math.Cos(angle) } });
            Matrix rotation_x = new Matrix(new double[,] { { 1, 0, 0 }, { 0, Math.Cos(angle), -Math.Sin(angle) }, { 0, Math.Sin(angle), Math.Cos(angle) } });
            Matrix temp = matrixVector.multiply(rotation_z);
            temp = temp.multiply(rotation_y);
            temp = temp.multiply(rotation_x);
            x = temp.tablica[0, 0];
            y = temp.tablica[1, 0];
            z = temp.tablica[2, 0];
            angle += 0.01;
        }
    }
}
