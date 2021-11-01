using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{
    class Matrix
    {
        public int[,] tablica;
        
        public Matrix(int [,] tablica)
        {
            this.tablica = tablica;
        }

        public Matrix multiply(Matrix m)
        {

            Matrix product = new Matrix(new int[m.tablica.GetLength(0), m.tablica.GetLength(1)]);
            
                for(int i = 0; i < this.tablica.GetLength(1); i++)
                {
                    for(int j = 0; j < this.tablica.GetLength(0); j++)
                    {
                        for(int k = 0; k < m.tablica.GetLength(1); k++)
                        {
                        Console.Write(i);
                        Console.Write(k);
                        product.tablica[i, k] +=
                            (m.tablica[k, j]
                            * 
                            this.tablica[j, i]); 
                        }
                    }
                }
            
            return product;
        }
    }
}
