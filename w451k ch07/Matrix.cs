using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duck
{
    class Matrix
    {
        public double[,] tablica;
        
        public Matrix(double[,] tablica)
        {
            this.tablica = tablica;
        }

        public Matrix multiply(Matrix m)
        {

            Matrix product = new Matrix(new double[m.tablica.GetLength(0), m.tablica.GetLength(1)]);
            for (int i = 0; i < this.tablica.GetLength(0); i++)
            {
                for (int k = 0; k < m.tablica.GetLength(1); k++)
                {
                    for (int j = 0; j < this.tablica.GetLength(1); j++)
                    {

                        product.tablica[i, k] += (m.tablica[j, k] * this.tablica[i, j]);
                    }
                }
            }

            return product;
        }
        public Matrix subtract(Matrix m)
        {
            Matrix product = new Matrix(new double[m.tablica.GetLength(0), m.tablica.GetLength(1)]);
            for (int i = 0; i < this.tablica.GetLength(0); ++i)
            {
                for (int j = 0; j < this.tablica.GetLength(1); ++j)
                {
                    product.tablica[i, j] = this.tablica[i, j] - m.tablica[i, j];
                }
            }
            return product;
        }
    }
}
