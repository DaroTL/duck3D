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
            Renderer zzzz = new Renderer();
            bool rise = true;
            for (int i = 10; ; )
            {
                zzzz.drawRectangle(new Triangle2(new Line2(10, 10, 20, 10), new Line2(20, 10, 20, i)), new Triangle2(new Line2(20, i, 10, i), new Line2(10, i, 10, 10)));
                zzzz.renderFrame();
                System.Threading.Thread.Sleep(5);
                if (i == 20) rise = false;
                if (i == 10) rise = true;
                if (rise) i++;
                else i--;
            }

//             for(; ; )
//             {
//                 String command = Console.ReadLine();
//                 switch (command)
//                 {
// 
//                 }
//             }
            int[] pen = usePen();




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
