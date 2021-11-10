using System;
using System.Linq;



namespace w451k_ch07
{
    public class Renderer
    {

        public char background = ' ',
                    fill = '+',
                    contour = '#';
        // 71 235
        public char[,] screen = new char[71, 235];


        int offsetX;
        int offsetY;
        int horizontalOffset = 2;

        public Renderer(char n_background, char n_fill, char n_contour, int screenWidth, int screenHeight)
        {
            screen = new char[screenWidth, screenHeight];
            background = n_background;
            fill = n_fill;
            contour = n_contour;
            offsetX = screen.GetLength(1) / 2;
            offsetY = screen.GetLength(0) / 2;
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int z = 0; z < screen.GetLength(1); z++)
                {
                    screen[i, z] = background;
                }
            }

        }
        public Renderer()
        {
            offsetX = screen.GetLength(1) / 2;
            offsetY = screen.GetLength(0) / 2;
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int z = 0; z < screen.GetLength(1); z++)
                {
                    screen[i, z] = background;
                }
            }

        }



        public void render(int[,] thing)
        {
            for(int i = 0; i < thing.GetLength(0); i++)
            {
                screen[thing[i,0],thing[i,1]] = contour;
            }
           
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int z = 0; z < screen.GetLength(1); z++)
                {
                    FastConsole.Write("" + screen[i, z]);
                }
                FastConsole.WriteLine("");
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

        public void renderFastAsFuck()
        {

            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < screen.GetLength(0); i++)
            {

                for (int z = 0; z < screen.GetLength(1); z++)
                {

                    FastConsole.Write("" + screen[i, z]);
                }
                FastConsole.WriteLine("");
            }
            FastConsole.Flush();



        }
        public void renderFastAsFuckFrame()
        {

            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < screen.GetLength(0); i++)
            {

                for (int z = 0; z < screen.GetLength(1); z++)
                {

                    FastConsole.Write("" + screen[i, z]);
                }
                FastConsole.WriteLine("");
            }
            for (int u = 0; u < screen.GetLength(0); u++)
            {
                for (int z = 0; z < screen.GetLength(1); z++)
                {
                    screen[u, z] = background;
                }
            }
            FastConsole.Flush();



        }

        public void ClearScreen()
        {
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
                addToScreen(y, x, fill);
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
                addToScreen(y, x, fill);
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
                    addToScreen(y,x,fill);
                    
                }
            }
            else
            {
                for (int x = x2; x <= x1; x++)
                {
                    addToScreen(y, x, fill);
                    
                }
            }
   
        }

        public void plotVerticalLine( int y1, int y2, int x)
        {
            
            if (y1 < y2)
            {
                
                for (int y = y1; y <= y2; y++)
                {
                    addToScreen(y, x, fill);
                    
                    
                }
            }
            else
            {
                
                for (int y = y2; y <= y1; y++)
                {
                    addToScreen(y, x, fill);
                    
                }
            }
        }

        public void plotLine(Line2 line)
        {
            int y1 = line.v1.y* horizontalOffset, y2 = line.v2.y * horizontalOffset;
            if(Math.Abs(y2 - y1) < Math.Abs(line.v2.x - line.v1.x))
            {
                if(line.v2.y == line.v1.y)
                {
                      plotHorizontalLine(line.v1.x, line.v2.x, y1);

                    return;
                }
                if(line.v1.x > line.v2.x)
                {
                    plotLineLow(new Line2(line.v2.x, y2, line.v1.x, y1)); ;
                }
                else
                {
                    plotLineLow(new Line2(line.v1.x, y1, line.v2.x, y2));
                }

            }
            else
            {
                if (line.v2.x == line.v1.x)
                {

                        plotVerticalLine(y1, y2, line.v1.x);

                    return;
                }
                if (line.v1.y > line.v2.y)
                {
                    plotLineHigh(new Line2(line.v2.x, y2, line.v1.x, y1));
                }
                else
                {
                    plotLineHigh(new Line2(line.v1.x, y1, line.v2.x, y2));
                }
            }


        }
        #region fille
        public void FillSimple(Vector2 v)
        {
            if (screen[v.x, v.y] != contour && screen[v.x, v.y] != fill)
            {
                addToScreen(v.x, v.y, fill);
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
        #endregion fille
        public void drawRectangleRaw(Pane2 p)
        {
            plotLine(p.lines[0]);
            plotLine(p.lines[1]);
            plotLine(p.lines[2]);
            plotLine(p.lines[3]);
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
    
        public void addToScreen(int x, int y, char addVar)
        {

            if((offsetY + y < screen.GetLength(0) && offsetY + y > 0) && (offsetX + x < screen.GetLength(1) && offsetX + x > 0))
            {
                screen[offsetY + y, offsetX + x] = addVar;
            }






        }
        public char getScreenField(int x, int y)
        {
            return screen[y + offsetY, x + offsetX];
        }




       public void DDAplotLine(Line2 line)
        {
            int x1 = line.v1.x, x2 = line.v2.x, y1 = line.v1.y * horizontalOffset, y2 = line.v2.y * horizontalOffset;
            int x = 0, y = 0;
            int dx = x2 - x1;
            int dy = y2 - y1;
            int step = 0;
            if (Math.Abs(dx) >= Math.Abs(dy))
            {
                step = Math.Abs(dx);
            }
            else step = Math.Abs(dy);

            dx = dx / step;
            dy = dy / step;

            x = x1;
            y = y1;
            for(int i = 1; i <= step; i++)
            {
                addToScreen(x, y, fill);
                x += dx;
                y += dy;   
            }
        }
    }

}
