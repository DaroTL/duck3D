using System;
using System.Linq;



namespace w451k_ch07
{
    class Renderer
    {

        public char background = ' ',
                    fill = '+',
                    contour = '#';

        public char[,] screen = new char[50,100];

        public Renderer()
        {
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int z = 0; z < screen.GetLength(1); z++)
                {
                    screen[i, z] = background;
                }
            }
        }

        public void loadOnScreen(int[,] thing)
        {
            for (int i = 0; i < thing.GetLength(0); i++)
            {
                screen[thing[i, 0], thing[i, 1]] = '#';
            }
        }

        public void render(int[,] thing)
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

        public void render()
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

        public void renderFrame()
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
            for (int u = 0; u < screen.GetLength(0); u++)
            {
                for (int z = 0; z < screen.GetLength(1); z++)
                {
                    screen[u, z] = background;
                }
            }
        }

        public void plotLineLow(Line2 line)
        {
            int dx = line.v2.x - line.v1.x,
                dy = line.v2.y - line.v1.y,
                yi = 1,
                D, y;
            if(dy < 0)
            {
                yi = -1;
                dy = -dy;
            }
            D = (2 * dy) - dx;
            y = line.v1.y;
            for (int x = line.v1.x; x < line.v2.x; x++)
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

        public void plotLineHigh(Line2 line)
        {
            int dx = line.v2.x - line.v1.x,
                dy = line.v2.y - line.v1.y,
                xi = 1,
                D, x;
            if (dx < 0)
            {
                xi = -1;
                dx = -dx;
            }
            D = (2 * dx) - dy;
            x = line.v1.x;
            for (int y = line.v1.y; y < line.v2.y; y++)
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

        public void plotHorizontalLine(int x1, int x2, int y)
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

        public void plotVerticalLine( int y1, int y2, int x)
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

        public void plotLine(Line2 line)
        {
            if(Math.Abs(line.v2.y - line.v1.y) < Math.Abs(line.v2.x - line.v1.x))
            {
                if(line.v2.y == line.v1.y)
                {
                      plotHorizontalLine(line.v1.x, line.v2.x, line.v1.y);

                    return;
                }
                if(line.v1.x > line.v2.x)
                {
                    plotLineLow(new Line2(line.v2, line.v1));
                }
                else
                {
                    plotLineLow(new Line2(line.v1, line.v2));
                }

            }
            else
            {
                if (line.v2.x == line.v1.x)
                {

                        plotVerticalLine(line.v1.y, line.v2.y, line.v1.x);

                    return;
                }
                if (line.v1.y > line.v2.y)
                {
                    plotLineHigh(new Line2(line.v2, line.v1));
                }
                else
                {
                    plotLineHigh(new Line2(line.v1, line.v2));
                }
            }


        }

        public void FillSimple(Vector2 v)
        {
            if (screen[v.x, v.y] != contour && screen[v.x, v.y] != fill)
            {
                screen[v.x, v.y] = fill;
                FillSimple(new Vector2(v.x + 1, v.y));
                FillSimple(new Vector2(v.x, v.y + 1));
                FillSimple(new Vector2(v.x - 1, v.y));
                FillSimple(new Vector2(v.x, v.y - 1));
                
            }
        }

        public void dFillSimple(Vector2 v)
        {
            Console.WriteLine(v.x + " " + v.y + screen[v.x, v.y]);
            if (screen[v.x, v.y] != contour && screen[v.x, v.y] != fill)
            {
                screen[v.x, v.y] = fill;
                String asodfjfoas = Console.ReadLine();
                render();
                Console.WriteLine(v.x + " " + v.y);

                dFillSimple(new Vector2(v.x + 1, v.y));
                dFillSimple(new Vector2(v.x, v.y + 1));
                dFillSimple(new Vector2(v.x - 1, v.y));
                dFillSimple(new Vector2(v.x, v.y - 1));

            }
        }

        public void Fill8(Vector2 v)
        {
            if (screen[v.x, v.y] != contour && screen[v.x, v.y] != fill)
            {
                screen[v.x, v.y] = fill;
                Fill8(new Vector2(v.x + 1, v.y));
                Fill8(new Vector2(v.x, v.y + 1));
                Fill8(new Vector2(v.x - 1, v.y));
                Fill8(new Vector2(v.x, v.y - 1));
                Fill8(new Vector2(v.x - 1, v.y - 1));
                Fill8(new Vector2(v.x - 1, v.y + 1));
                Fill8(new Vector2(v.x + 1, v.y - 1));
                Fill8(new Vector2(v.x + 1, v.y + 1));

            }
        }

        public void drawRectangleRaw(Pane2 p)
        {
            plotLine(p.l1);
            plotLine(p.l2);
            plotLine(p.l3);
            plotLine(p.l4);
            int[] x = { p.l1.v1.x, p.l2.v1.x, p.l3.v1.x, p.l4.v1.x };
            int[] y = { p.l1.v1.y, p.l2.v1.y, p.l3.v1.y, p.l4.v1.y };
            FillSimple(new Vector2(x.Min() + (x.Max() / 2), y.Min() + (y.Max() / 2)));

        }

        public void drawRectangle(Triangle2 t1, Triangle2 t2)
        {
            plotLine(t1.l1);
            plotLine(t1.l2);
            plotLine(t1.l3);
            plotLine(t2.l1);
            plotLine(t2.l2);
            plotLine(t2.l3);
        }

        public void drawTriangle(Triangle2 t)
        {
            plotLine(t.l1);
            plotLine(t.l2);
            plotLine(t.l3);
        }

        public void drawXgon(Vector2[] points)
        {
            if(points.GetLength(0) > 2)
            {
                int[] x = new int[points.GetLength(0)],y = new int[points.GetLength(0)];
                for (int m = 0; m < points.GetLength(0); m++)
                {
                    if(m != points.GetLength(0) - 1)
                    {
                         plotLine(new Line2(points[m], points[m + 1]));
                    }
                    else
                    {
                        plotLine(new Line2(points[m], points[0]));
                    }

                    x[m] = points[m].x;
                    y[m] = points[m].y;
                }
                 
            }
        }

        public int[] getPointInShape(int minX, int minY)
        {
            return new int[] {0,0};
        }
    }

}
