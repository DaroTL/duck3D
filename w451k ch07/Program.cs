using System;
using System.Linq;



namespace w451k_ch07
{
    class Program
    {

        static char background = ' ',
                    fill = '+',
                    contour = '#';
       
        static char[,] screen = new char[50,100];
        static void Main(string[] args)
        {
            Console.WriteLine(screen.GetLength(0));
            Console.WriteLine(screen.GetLength(1));
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int z = 0; z < screen.GetLength(1); z++)
                {
                    screen[i, z] = background;
                }
            }



            Console.Beep();

            drawXgon(new int[,] { { 10, 10 }, { 40, 40 }, { 10, 20 } });

            render();

        }

        static void loadOnScreen(int[,] thing)
        {
            for (int i = 0; i < thing.GetLength(0); i++)
            {
                screen[thing[i, 0], thing[i, 1]] = '#';
            }
        }

        static void render(int[,] thing)
        {
            for(int i = 0; i < thing.GetLength(0); i++)
            {
                screen[thing[i,0],thing[i,1]] = contour;
            }
            Console.Clear();
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int z = 0; z < screen.GetLength(1); z++)
                {
                    Console.Write(screen[i, z]);
                }
                Console.WriteLine();
            }
        }

        static void render()
        {
            Console.Clear();
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int z = 0; z < screen.GetLength(1); z++)
                {
                    Console.Write(screen[i, z]);
                }
                Console.WriteLine();
            }
        }

        static void plotLineLow(int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1,
                dy = y2 - y1,
                yi = 1,
                D, y;
            if(dy < 0)
            {
                yi = -1;
                dy = -dy;
            }
            D = (2 * dy) - dx;
            y = y1;
            for (int x = x1; x < x2; x++)
            {
                loadOnScreen(new int[,] { { x,y} });
                if (D > 0)
                {
                    y = y + yi;
                    D = D + (2 * (dy - dx));
                }
                else
                {
                    D = D + 2 * dy;
                }
            }
            
      
        }

        static void plotLineHigh(int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1,
                dy = y2 - y1,
                xi = 1,
                D, x;
            if (dx < 0)
            {
                xi = -1;
                dx = -dx;
            }
            D = (2 * dx) - dy;
            x = x1;
            for (int y = y1; y < y2; y++)
            {
                loadOnScreen(new int[,] { { x,y } });
                if (D > 0)
                {
                    x = x + xi;
                    D = D + (2 * (dx - dy));
                }
                else
                {
                    D = D + 2 * dx;
                }
            }


        }

        static void plotHorizontalLine(int x1, int x2, int y)
        {
            if(x1 < x2)
            {
                for (int x = x1; x <= x2; x++)
                {
                    loadOnScreen(new int[,] { { x, y } });
                    
                }
            }
            else
            {
                for (int x = x2; x <= x1; x++)
                {
                    loadOnScreen(new int[,] { { x, y } });
                    Console.WriteLine(x);
                   
                }
            }
   
        }

        static void plotVerticalLine( int y1, int y2, int x)
        {
            
            if (y1 < y2)
            {
                
                for (int y = y1; y <= y2; y++)
                {
                    loadOnScreen(new int[,] { { x, y } });
                    
                }
            }
            else
            {
                
                for (int y = y2; y <= y1; y++)
                {
                    
                    loadOnScreen(new int[,] { { x, y } });
                }
            }
        }

        static void plotLine(int x1, int y1, int x2, int y2)
        {
            if(Math.Abs(y2 - y1) < Math.Abs(x2 - x1))
            {
                if(y2 == y1)
                {
                      plotHorizontalLine(x1, x2, y1);

                    return;
                }
                if(x1 > x2)
                {
                    plotLineLow(x2, y2, x1, y1);
                }
                else
                {
                    plotLineLow(x1, y1, x2, y2);
                }

            }
            else
            {
                if ( x2 == x1)
                {

                        plotVerticalLine(y1, y2, x1);

                    return;
                }
                if (y1 > y2)
                {
                    plotLineHigh(x2, y2, x1, y1);
                }
                else
                {
                    plotLineHigh(x1, y1, x2, y2);
                }
            }


        }

        static void FillSimple(int x, int y)
        {
            if (screen[x, y] != contour && screen[x,y] != fill)
            {
                screen[x, y] = fill;
                FillSimple(x + 1, y);
                FillSimple(x, y + 1);
                FillSimple(x - 1, y);
                FillSimple(x, y - 1);
                
            }
        }

        static void dFillSimple(int x, int y)
        {
            Console.WriteLine(x + " " + y + screen[x, y]);
            if (screen[x, y] != contour && screen[x, y] != fill)
            {
                screen[x, y] = fill;
                String asodfjfoas = Console.ReadLine();
                render();
                Console.WriteLine(x + " " + y);

                dFillSimple(x + 1, y);
                dFillSimple(x, y + 1);
                dFillSimple(x - 1, y);
                dFillSimple(x, y - 1);

            }
        }

        static void Fill8(int x, int y)
        {
            if (screen[x, y] != contour && screen[x, y] != fill)
            {
                screen[x, y] = fill;
                Fill8(x + 1, y);
                Fill8(x, y + 1);
                Fill8(x - 1, y);
                Fill8(x, y - 1);
                Fill8(x - 1, y - 1);
                Fill8(x - 1, y + 1);
                Fill8(x + 1, y - 1);
                Fill8(x + 1, y + 1);

            }
        }



        static void drawRectangle(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            plotLine(x1, y1, x2, y2);
            plotLine(x2, y2, x3, y3);
            plotLine(x3, y3, x4, y4);
            plotLine(x4, y4, x1, y1);
            int[] x = { x1, x2, x3, x4 };
            int[] y = { y1, y2, y3, y4 };
            FillSimple(x.Min() + (x.Max() / 2), y.Min() + (y.Max() / 2));

        }

        static void drawXgon(int[,] points)
        {
            if(points.GetLength(0) > 2)
            {
                int[] x = new int[points.GetLength(0)],y = new int[points.GetLength(0)];
                for (int m = 0; m < points.GetLength(0); m++)
                {
                    if(m != points.GetLength(0) - 1)
                    {
                        plotLine(points[m, 0], points[m, 1], points[m + 1, 0], points[m + 1, 1]);
                    }
                    else
                    {
                        plotLine(points[m, 0], points[m, 1], points[0, 0], points[0, 1]);
                    }

                    x[m] = points[m, 0];
                    y[m] = points[m, 1];
                }
                 
            }
        }

        static int[] getPointInShape(int minX, int minY)
        {
            return new int[] {0,0};
        }
    }

}
