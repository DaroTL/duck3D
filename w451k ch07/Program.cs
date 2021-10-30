using System;


namespace w451k_ch07
{
    public class Program
    {
        static void Main(string[] args)
        {
            Renderer zzzz = new Renderer();
            bool rise = true;
            Vector3 v1 = new Vector3(20, 20, 20);
            Vector3 v2 = new Vector3(20, 60, 20);
            Vector3 v3 = new Vector3(60, 60, 20);
            Vector3 v4 = new Vector3(60, 20, 20);
            Vector3 v5 = new Vector3(60, 20, 60);
            Vector3 v6 = new Vector3(20, 20, 60);
            Vector3 v7 = new Vector3(20, 60, 60);
            Vector3 v8 = new Vector3(60, 60, 60);
            Line3 l1 = new Line3(v1, v2);
            Line3 l2 = new Line3(v2, v3);
            Line3 l3 = new Line3(v3, v4);
            Line3 l4 = new Line3(v4, v1);
            Line3 l5 = new Line3(v4, v5);
            Line3 l6 = new Line3(v5, v6);
            Line3 l7 = new Line3(v6, v1);
            Line3 l8 = new Line3(v6, v7);
            Line3 l9 = new Line3(v7, v2);
            Line3 l10 = new Line3(v7, v8);
            Line3 l11 = new Line3(v8, v3);
            Line3 l12 = new Line3(v8, v5);
            
            Pane3 pane3d1 = new Pane3(l1, l2, l3, l4);
            Pane3 pane3d2 = new Pane3(l4, l5, l6, l7);
            Pane3 pane3d3 = new Pane3(l7, l8, l9, l1);
            Pane3 pane3d4 = new Pane3(l9, l2, l11, l10);
            Pane3 pane3d5 = new Pane3(l5, l11, l3, l12);
            Pane3 pane3d6 = new Pane3(l6, l12, l10, l8);
            zzzz.drawRectangleRaw(Pane2.convertPane3(pane3d1));
            zzzz.drawRectangleRaw(Pane2.convertPane3(pane3d2));
            zzzz.drawRectangleRaw(Pane2.convertPane3(pane3d3));
            zzzz.drawRectangleRaw(Pane2.convertPane3(pane3d4));
            zzzz.drawRectangleRaw(Pane2.convertPane3(pane3d5));
            zzzz.drawRectangleRaw(Pane2.convertPane3(pane3d6));
            zzzz.render();
            /*
            for (int i = 30; ; )
            {
                zzzz.drawRectangle(new Triangle2(new Line2(25, 30, 45, 30), new Line2(45, 30, 40, i)), new Triangle2(new Line2(40, i, 30, i), new Line2(30, i, 25, 30)));
                zzzz.renderFrame();
                System.Threading.Thread.Sleep(10);
                if (i == 40) rise = false;
                if (i == 20) rise = true;
                if (rise) i++;
                else i--;
            }
            */


                       
//             bool rise = true;
//             for (int i = 120; ; )
//             {
//                 if (Console.KeyAvailable) break;
//                 zzzz.drawRectangle(new Triangle2(new Line2(20, 120, 100, 120), new Line2(100, 120, 80, i)), new Triangle2(new Line2(80, i, 40, i), new Line2(40, i, 20, 120)));
//                 System.Threading.Thread.Sleep(10);
//                 zzzz.renderFastAsFuck();
// 
//                  if (i == 300) rise = false;
//                  if (i == 20) rise = true;
//                  if (rise) i++;
//                  else i--;
// 
//             }

            for(; ; )
            {
                String command = Console.ReadLine();
                switch (command)
                {
                    case "point":
                        {
                            int[] pen = usePen();
                            Console.Clear();
                            Console.SetCursorPosition(0, 0);
                            for (int i = 0; i < pen.Length; i++)
                            {
                                Console.Write(pen[i] + " ");
                            }
                            Console.WriteLine();
                        }
                        break;
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("no such command");
                        }
                        break;
                }
            }
            




        }


        static int[] usePen()
        {
            for (; ; )
            {
                ConsoleKeyInfo pen = Console.ReadKey();

                int y = Console.CursorTop, x = Console.CursorLeft;
                if (pen.Key == ConsoleKey.F1) return new int[] { x, y };
                
                if (pen.Key == ConsoleKey.DownArrow)
                {
                    if (y >= (y + 1))
                        Console.SetCursorPosition(x, y - 1 + 1);
                    else
                        Console.SetCursorPosition(x - 1, y + 1);

                }

                if (pen.Key == ConsoleKey.UpArrow)
                {
                    if (y <= 0)
                        Console.SetCursorPosition(x, y + 1 - 1);
                    else
                        Console.SetCursorPosition(x - 1, y - 1);

                }

                if (pen.Key == ConsoleKey.RightArrow)
                {
                    if (x >= (x + 1))
                        Console.SetCursorPosition(x - 2 + 1, y);
                    else
                        Console.SetCursorPosition(x + 1, y);

                }

                if (pen.Key == ConsoleKey.LeftArrow)
                {
                    if (x <= 0)
                        Console.SetCursorPosition(x + 1 - 1, y);
                    else
                        Console.SetCursorPosition(x - 2, y);

                }

                if (x > Console.WindowWidth) Console.SetCursorPosition(x, (y + 1));
                if (pen.Modifiers == ConsoleModifiers.Alt && pen.Key == ConsoleKey.F4) Environment.Exit(0);
                if (pen.Key == ConsoleKey.Escape) break;

                 
            }
            return new int[]  { 0, 0 };
        }
    }
}
