using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{
    class Program
    {
        static void Main(string[] args)
        {
            Renderer x = new Renderer();

            x.plotLine(20, 20, 10, 20);
            x.render();

        }
    }
}
