using System;
using System.Linq;
using System.Collections.Generic;



namespace w451k_ch07
{
    public class Renderer
    {

        public char background = ' ',
                    fill = '+',
                    contour = '#';
        // 71 235
        public char[,] screen = new char[200, 500];


        int offsetX;
        int offsetY;
        int horizontalOffset = 2;
        public float scale = 1;

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
            float dx = line.v2.x - line.v1.x,
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
            for (int x = (int)line.v1.x; x < line.v2.x; x++)
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
            float dx = line.v2.x - line.v1.x,
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
            for (int y = (int)line.v1.y; y < line.v2.y; y++)
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

        public void plotHorizontalLine(float x1, float x2, float y)
        {
            if(x1 < x2)
            {
                for (int x = (int)x1; x <= x2; x++)
                {
                    addToScreen(y,x,fill);
                    
                }
            }
            else
            {
                for (int x = (int)x2; x <= x1; x++)
                {
                    addToScreen(y, x, fill);
                    
                }
            }
   
        }

        public void plotVerticalLine( float y1, float y2, float x)
        {
            
            if (y1 < y2)
            {
                
                for (int y = (int)y1; y <= y2; y++)
                {
                    addToScreen(y, x, fill);
                    
                    
                }
            }
            else
            {
                
                for (int y = (int)y2; y <= y1; y++)
                {
                    addToScreen(y, x, fill);
                    
                }
            }
        }

        public void plotLine(Line2 line)
        {
            float y1 = line.v1.y* horizontalOffset * scale, y2 = line.v2.y * horizontalOffset * scale;
            float x1 = line.v1.x * scale, x2 = line.v2.x * scale;
            if (Math.Abs(y2 - y1) < Math.Abs(line.v2.x - line.v1.x))
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

        public void FillTriangle(Vector2 p0, Vector2 p1, Vector2 p2, char col)
        {

            Vector2 v0 = new Vector2(p0.x * horizontalOffset * scale, p0.y * scale);
            Vector2 v1 = new Vector2(p1.x * horizontalOffset * scale, p1.y * scale);
            Vector2 v2 = new Vector2(p2.x * horizontalOffset * scale, p2.y * scale);

            if (v1.y < v0.y) swap(ref v0, ref v1);
            if(v2.y < v1.y) swap(ref v1, ref v2);
            if(v1.y < v0.y) swap(ref v0, ref v1);



            if (v0.y == v1.y)
            {

                if (v1.x < v0.x) swap(ref v0, ref v1);
                DrawFlatTopTriangle(v0, v1, v2, col);
            }
            else if(v1.y == v2.y)
            {


                if (v2.x < v1.x) swap(ref v1, ref v2);
                DrawFlatBottomTriangle(v0, v1, v2, col);
            }
            else
            {

                float alphaSplit = (v1.y - v0.y) / (v2.y - v0.y);

                Vector2 vi = new Vector2(
                    (v0.x + (v2.x - v0.x) * alphaSplit),
                    (v0.y + (v2.y - v0.y) * alphaSplit)
                    );

                if(v1.x < vi.x)
                {
                    DrawFlatBottomTriangle(v0, v1, vi, col);
                    DrawFlatTopTriangle(v1, vi, v2, col);
                }
                else
                {
                    DrawFlatBottomTriangle(v0, vi, v1, col);
                    DrawFlatTopTriangle(vi, v1, v2, col);
                }
            }
        }

        private void DrawFlatTopTriangle(Vector2 v0, Vector2 v1, Vector2 v2, char col)
        {


            float m0 = (v2.x - v0.x) / (v2.y - v0.y);
            float m1 = (v2.x - v1.x) / (v2.y - v1.y);

            int yStart = (int)Math.Ceiling(v0.y - 0.5f);
            int yEnd = (int)Math.Ceiling(v2.y - 0.5f);

            for (int y = yStart; y < yEnd; y++)
            {
                float px0 = m0 * ((float)y + 0.5f - v0.y) + v0.x;
                float px1 = m1 * ((float)y + 0.5f - v1.y) + v1.x;

                int xStart = (int)Math.Ceiling(px0 - 0.5f);
                int xEnd = (int)Math.Ceiling(px1 - 0.5f);

                for( int x = xStart; x < xEnd; x++)
                {


                    addToScreen(x, y, col);
                }
            }

        }

        private void DrawFlatBottomTriangle(Vector2 v0, Vector2 v1, Vector2 v2, char col)
        {
            float m0 = (v1.x - v0.x) / (v1.y - v0.y);
            float m1 = (v2.x - v0.x) / (v2.y - v0.y);

            int yStart = (int)Math.Ceiling(v0.y - 0.5f);
            int yEnd = (int)Math.Ceiling(v2.y - 0.5f);

            for (int y = yStart; y < yEnd; y++)
            {
                float px0 = m0 * ((float)y + 0.5f - v0.y) + v0.x;
                float px1 = m1 * ((float)y + 0.5f - v0.y) + v0.x;

                int xStart = (int)Math.Ceiling(px0 - 0.5f);
                int xEnd = (int)Math.Ceiling(px1 - 0.5f);

                for (int x = xStart; x < xEnd; x++)
                {

                    addToScreen(x, y, col);
                }
            }
        }

/*        public void FillSimple(Vector2 v)
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
        }*/
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


/*        public void drawXgon(Vector2[] points)
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
        }*/

        public int[] getPointInShape(int minX, int minY)
        {
            return new int[] {0,0};
        }
    
        public void addToScreen(float x, float y, char addVar)
        {

            if((offsetY + y < screen.GetLength(0) && offsetY + y > 0) && (offsetX - x < screen.GetLength(1) && offsetX - x > 0))
            {
                screen[offsetY + (int)Math.Ceiling(y), (offsetX - (int)Math.Ceiling(x))] = addVar;
            }






        }
        public char getScreenField(int x, int y)
        {
            return screen[y + offsetY, x + offsetX];
        }


        void swap(ref Vector2 v1, ref Vector2 v2)
        {
            Vector2 v3 = v1;

             v1 = v2;
             v2 = v3;
        }


        List<int> Interpolate(int i0, int d0, int i1, int d1) {
          if (i0 == i1) {
            return new List<int>() { d0};
          }

            List<int> values = new List<int>();
            float a = (d1 - d0) / (i1 - i0);
            float d = d0;
          for (int i = i0; i <= i1; i++) {
            values.Add((int)d);
            d += a;
          }

        return values;
        }
    }

}
