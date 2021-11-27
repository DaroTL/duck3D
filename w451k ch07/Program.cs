using System;
using System.Collections.Generic;
using System.Threading;

namespace w451k_ch07
{
    public class Program
    {
        static List<Object> objects = new List<Object>();
        static Renderer render;
        static void Main(string[] args)
        {
            
            render = new Renderer();
            Object cube = new Object("cube", new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            objects.Add(cube);
            cube.addVert(new Point3D(cube.location, 0, 0, 0, 100));
            cube.addVert(new Point3D(cube.location, 30, 0, 0, 101));
            cube.addVert(new Point3D(cube.location, 30, 30, 0, 102));
            cube.addVert(new Point3D(cube.location, 0, 30, 0, 103));
            cube.addVert(new Point3D(cube.location, 0, 0, 30, 110));
            cube.addVert(new Point3D(cube.location, 30, 0, 30, 111));
            cube.addVert(new Point3D(cube.location, 30, 30, 30, 112));
            cube.addVert(new Point3D(cube.location, 0, 30, 30, 113));
            cube.connectVerts(new int[] { 100, 101 });
            cube.connectVerts(new int[] { 101, 102 });
            cube.connectVerts(new int[] { 102, 103 });
            cube.connectVerts(new int[] { 103, 100 });
            cube.connectVerts(new int[] { 110, 111 });
            cube.connectVerts(new int[] { 111, 112 });
            cube.connectVerts(new int[] { 112, 113 });
            cube.connectVerts(new int[] { 113, 110 });
            cube.connectVerts(new int[] { 110, 100 });
            cube.connectVerts(new int[] { 111, 101 });
            cube.connectVerts(new int[] { 112, 102 });
            cube.connectVerts(new int[] { 113, 103 });

            foreach (Line3 x in cube.lines)
            {
                render.plotLine(new Line2(x.p1.convertVectorTo2D(), x.p2.convertVectorTo2D()));
            }
            render.renderFastAsFuck();
            Thread rend = new Thread(startrnd);
            
            //rend.Start();
            for (; ; )
            {
                string command = Console.ReadLine();
                string[] commandd = command.Split(' ');
                Console.Clear();
                switch (commandd[0])
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
                    case "show":
                        {
                            if (commandd[1] == "objects")
                            {
                                foreach (Object x in objects)
                                {
                                    Console.WriteLine(x.name + "      position: ( x: " + x.location.x + " y: " + x.location.y + " z: " + x.location.z + " )");
                                }
                            }
                        }
                        break;
                    case "transform":
                        bool active = true;
                        if (commandd.Length == 5)
                        {
                            foreach (Object x in objects)
                            {
                                if (x.name == commandd[1])
                                {
                                    render.ClearScreen();
                                    x.transformGlobal(new Vector3(-Convert.ToDouble(commandd[2]), -Convert.ToDouble(commandd[3]), -Convert.ToDouble(commandd[4])));
                                    foreach (Line3 z in x.lines)
                                    {
                                        render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                    }
                                    render.renderFastAsFuck();
                                }
                                else
                                {
                                    render.renderFastAsFuck();
                                }

                            }

                        }
                        if (commandd.Length == 2)
                        {
                            while (active == true)
                            {
                            var key = Console.ReadKey(false).Key;
                                switch (key)
                                {
                                    case ConsoleKey.LeftArrow:
                                        foreach (Object x in objects)
                                        {
                                            if (x.name == commandd[1])
                                            {
                                                render.ClearScreen();
                                                x.transformGlobal(new Vector3(0, 1, 0));
                                                foreach (Line3 z in x.lines)
                                                {
                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));

                                                }
                                                render.renderFastAsFuck();
                                            }
                                            else
                                            {
                                                render.renderFastAsFuck();
                                            }
                                        }
                                        break;
                                    case ConsoleKey.DownArrow:
                                            foreach (Object x in objects)
                                            {
                                                if (x.name == commandd[1])
                                                {
                                                    render.ClearScreen();
                                                    x.transformGlobal(new Vector3(-1, 0, 0));
                                                    foreach (Line3 z in x.lines)
                                                    {
                                                        render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                    }
                                                    render.renderFastAsFuck();
                                                }
                                                else
                                                {
                                                    render.renderFastAsFuck();
                                                }
                                            }  
                                        break;
                                    case ConsoleKey.RightArrow:
                                        foreach (Object x in objects)
                                        {
                                            if (x.name == commandd[1])
                                            {
                                                render.ClearScreen();
                                                x.transformGlobal(new Vector3(0, -1, 0));
                                                foreach (Line3 z in x.lines)
                                                {
                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                }
                                                render.renderFastAsFuck();
                                            }
                                            else
                                            {
                                                render.renderFastAsFuck();
                                            }
                                        }
                                        break;
                                    case ConsoleKey.UpArrow:
                                        foreach (Object x in objects)
                                        {
                                            if (x.name == commandd[1])
                                            {
                                                render.ClearScreen();
                                                x.transformGlobal(new Vector3(1, 0, 0));
                                                foreach (Line3 z in x.lines)
                                                {
                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                }
                                                render.renderFastAsFuck();
                                            }
                                            else
                                            {
                                                render.renderFastAsFuck();
                                            }
                                        }
                                        break;
                                    case ConsoleKey.Enter:
                                        foreach (Object x in objects)
                                        {
                                            if (x.name == commandd[1])
                                            {
                                                render.ClearScreen();
                                                x.transformGlobal(new Vector3(0, 0, 1));
                                                foreach (Line3 z in x.lines)
                                                {
                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                }
                                                render.renderFastAsFuck();
                                            }
                                            else
                                            {
                                                render.renderFastAsFuck();
                                            }
                                        }
                                        break;
                                    case ConsoleKey.Backspace:
                                        foreach (Object x in objects)
                                        {
                                            if (x.name == commandd[1])
                                            {
                                                render.ClearScreen();
                                                x.transformGlobal(new Vector3(0, 0, -1));
                                                foreach (Line3 z in x.lines)
                                                {
                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                }
                                                render.renderFastAsFuck();
                                            }
                                            else
                                            {
                                                render.renderFastAsFuck();
                                            }
                                        }
                                        break;
                                    case ConsoleKey.Escape:
                                        active = false;
                                        break;
                                }
                            }
                           
                        }
                        break;
                    case "render":
                        {
                            render.renderFastAsFuck();
                        }
                        break;
                    case "rotate":
                        {
                            if (commandd.Length == 3 && commandd[1] == "transform")
                            {
                                bool act = true;
                                while (act)
                                {
                                    var key = Console.ReadKey(false).Key;
                                    switch (key)
                                    {
                                        case ConsoleKey.LeftArrow:
                                            {
                                                foreach (Object x in objects)
                                                {
                                                    if (x.name == commandd[2])
                                                    {
                                                        render.ClearScreen();
                                                        x.rotateX(0.05);
                                                        foreach (Line3 z in x.lines)
                                                        {
                                                            render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                        }
                                                        render.renderFastAsFuck();
                                                    }
                                                }
                                            }
                                            break;
                                        case ConsoleKey.RightArrow:
                                            {
                                                foreach (Object x in objects)
                                                {
                                                    if (x.name == commandd[2])
                                                    {
                                                        render.ClearScreen();
                                                        x.rotateX(-0.05);
                                                        foreach (Line3 z in x.lines)
                                                        {
                                                            render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                        }
                                                        render.renderFastAsFuck();
                                                    }
                                                }
                                            }
                                            break;
                                        case ConsoleKey.UpArrow:
                                            {
                                                foreach (Object x in objects)
                                                {
                                                    if (x.name == commandd[2])
                                                    {
                                                        render.ClearScreen();
                                                        x.rotateY(-0.05);
                                                        foreach (Line3 z in x.lines)
                                                        {
                                                            render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                        }
                                                        render.renderFastAsFuck();
                                                    }
                                                }
                                            }
                                            break;
                                        case ConsoleKey.DownArrow:
                                            {
                                                foreach (Object x in objects)
                                                {
                                                    if (x.name == commandd[2])
                                                    {
                                                        render.ClearScreen();
                                                        x.rotateY(0.05);
                                                        foreach (Line3 z in x.lines)
                                                        {
                                                            render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                        }
                                                        render.renderFastAsFuck();
                                                    }
                                                }
                                            }
                                            break;
                                        case ConsoleKey.A:
                                            foreach (Object x in objects)
                                            {
                                                if (x.name == commandd[2])
                                                {
                                                    render.ClearScreen();
                                                    x.transformGlobal(new Vector3(0, 1, 0));
                                                    foreach (Line3 z in x.lines)
                                                    {
                                                        render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));

                                                    }
                                                    render.renderFastAsFuck();
                                                }
                                                else
                                                {
                                                    render.renderFastAsFuck();
                                                }
                                            }
                                            break;
                                        case ConsoleKey.S:
                                            foreach (Object x in objects)
                                            {
                                                if (x.name == commandd[2])
                                                {
                                                    render.ClearScreen();
                                                    x.transformGlobal(new Vector3(-1, 0, 0));
                                                    foreach (Line3 z in x.lines)
                                                    {
                                                        render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                    }
                                                    render.renderFastAsFuck();
                                                }
                                                else
                                                {
                                                    render.renderFastAsFuck();
                                                }
                                            }
                                            break;
                                        case ConsoleKey.D:
                                            foreach (Object x in objects)
                                            {
                                                if (x.name == commandd[2])
                                                {
                                                    render.ClearScreen();
                                                    x.transformGlobal(new Vector3(0, -1, 0));
                                                    foreach (Line3 z in x.lines)
                                                    {
                                                        render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                    }
                                                    render.renderFastAsFuck();
                                                }
                                                else
                                                {
                                                    render.renderFastAsFuck();
                                                }
                                            }
                                            break;
                                        case ConsoleKey.W:
                                            foreach (Object x in objects)
                                            {
                                                if (x.name == commandd[2])
                                                {
                                                    render.ClearScreen();
                                                    x.transformGlobal(new Vector3(1, 0, 0));
                                                    foreach (Line3 z in x.lines)
                                                    {
                                                        render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                    }
                                                    render.renderFastAsFuck();
                                                }
                                                else
                                                {
                                                    render.renderFastAsFuck();
                                                }
                                            }
                                            break;
                                        case ConsoleKey.Enter:
                                            foreach (Object x in objects)
                                            {
                                                if (x.name == commandd[2])
                                                {
                                                    render.ClearScreen();
                                                    x.transformGlobal(new Vector3(0, 0, 1));
                                                    foreach (Line3 z in x.lines)
                                                    {
                                                        render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                    }
                                                    render.renderFastAsFuck();
                                                }
                                                else
                                                {
                                                    render.renderFastAsFuck();
                                                }
                                            }
                                            break;
                                        case ConsoleKey.Backspace:
                                            foreach (Object x in objects)
                                            {
                                                if (x.name == commandd[2])
                                                {
                                                    render.ClearScreen();
                                                    x.transformGlobal(new Vector3(0, 0, -1));
                                                    foreach (Line3 z in x.lines)
                                                    {
                                                        render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                    }
                                                    render.renderFastAsFuck();
                                                }
                                                else
                                                {
                                                    render.renderFastAsFuck();
                                                }
                                            }
                                            break;
                                        case ConsoleKey.Escape:
                                            act = false;
                                            break;
                                    }
                                }

                            }
                            else if (commandd.Length == 3)
                            {
                                if (commandd[2] == "local")
                                {
                                    foreach (Object x in objects)
                                    {
                                        if (x.name == commandd[1])
                                        {
                                            bool act = true;
                                            while (act)
                                            {
                                                var key = Console.ReadKey(false).Key;
                                                switch (key)
                                                {
                                                    case ConsoleKey.LeftArrow:
                                                        {
                                                            render.ClearScreen();
                                                            x.rotateLocal(new Vector3(0.05, 0, 0));
                                                            foreach (Line3 z in x.lines)
                                                            {
                                                                render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                            }
                                                            render.renderFastAsFuck();
                                                        }
                                                        break;
                                                    case ConsoleKey.RightArrow:
                                                        {
                                                            render.ClearScreen();

                                                            x.rotateLocal(new Vector3(-0.05, 0, 0));
                                                            foreach (Line3 z in x.lines)
                                                            {
                                                                render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                            }
                                                            render.renderFastAsFuck();
                                                        }
                                                        break;
                                                    case ConsoleKey.UpArrow:
                                                        {
                                                            render.ClearScreen();
                                                            x.rotateLocal(new Vector3(0, -0.05, 0));
                                                            foreach (Line3 z in x.lines)
                                                            {
                                                                render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                            }
                                                            render.renderFastAsFuck();
                                                        }
                                                        break;
                                                    case ConsoleKey.DownArrow:
                                                        {
                                                            render.ClearScreen();
                                                            x.rotateLocal(new Vector3(0, 0.05, 0));
                                                            foreach (Line3 z in x.lines)
                                                            {
                                                                render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                            }
                                                            render.renderFastAsFuck();
                                                        }
                                                        break;
                                                    case ConsoleKey.Escape:
                                                        act = false;
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (commandd[1] == "transform")
                                {
                                    foreach (Object x in objects)
                                    {
                                        if (x.name == commandd[2])
                                        {
                                            bool act = true;
                                            while (act)
                                            {
                                                var key = Console.ReadKey(false).Key;
                                                switch (key)
                                                {
                                                    case ConsoleKey.LeftArrow:
                                                        {
                                                            render.ClearScreen();
                                                            x.rotateLocal(new Vector3(0.05, 0, 0));
                                                            foreach (Line3 z in x.lines)
                                                            {
                                                                render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                            }
                                                            render.renderFastAsFuck();
                                                        }
                                                        break;
                                                    case ConsoleKey.RightArrow:
                                                        {
                                                            render.ClearScreen();

                                                            x.rotateLocal(new Vector3(-0.05, 0, 0));
                                                            foreach (Line3 z in x.lines)
                                                            {
                                                                render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                            }
                                                            render.renderFastAsFuck();
                                                        }
                                                        break;
                                                    case ConsoleKey.UpArrow:
                                                        {
                                                            render.ClearScreen();
                                                            x.rotateLocal(new Vector3(0, -0.05, 0));
                                                            foreach (Line3 z in x.lines)
                                                            {
                                                                render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                            }
                                                            render.renderFastAsFuck();
                                                        }
                                                        break;
                                                    case ConsoleKey.DownArrow:
                                                        {
                                                            render.ClearScreen();
                                                            x.rotateLocal(new Vector3(0, 0.05, 0));
                                                            foreach (Line3 z in x.lines)
                                                            {
                                                                render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                            }
                                                            render.renderFastAsFuck();
                                                        }
                                                        break;
                                                    case ConsoleKey.Escape:
                                                        act = false;
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (commandd[2] == "animate")
                                {

                                    foreach (Object x in objects)
                                    {
                                        if (x.name == commandd[1])
                                        {
                                            while (true)
                                            {
                                                render.ClearScreen();
                                                x.rotateLocal(new Vector3(-0.05, 0, 0));
                                                foreach (Line3 z in x.lines)
                                                {
                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                }
                                                render.renderFastAsFuck();
                                            }
                                        }
                                    }
                                }

                                else if (commandd.Length == 6)
                                {
                                    if (commandd[2] == "around")
                                    {
                                        foreach (Object x in objects)
                                        {
                                            if (x.name == commandd[1])
                                            {
                                                Vector3 point = new Vector3(-Convert.ToDouble(commandd[3]), -Convert.ToDouble(commandd[4]), -Convert.ToDouble(commandd[5]));
                                                bool act = true;
                                                while (act)
                                                {
                                                    var key = Console.ReadKey(false).Key;
                                                    switch (key)
                                                    {
                                                        case ConsoleKey.LeftArrow:
                                                            {
                                                                render.ClearScreen();
                                                                x.rotateAround(new Vector3(0.05, 0, 0), point);
                                                                foreach (Line3 z in x.lines)
                                                                {
                                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            break;
                                                        case ConsoleKey.RightArrow:
                                                            {
                                                                render.ClearScreen();
                                                                x.rotateAround(new Vector3(-0.05, 0, 0), point);
                                                                foreach (Line3 z in x.lines)
                                                                {
                                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            break;
                                                        case ConsoleKey.UpArrow:
                                                            {
                                                                render.ClearScreen();
                                                                x.rotateAround(new Vector3(0, -0.05, 0), point);
                                                                foreach (Line3 z in x.lines)
                                                                {
                                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            break;
                                                        case ConsoleKey.DownArrow:
                                                            {
                                                                render.ClearScreen();
                                                                x.rotateAround(new Vector3(0, 0.05, 0), point);
                                                                foreach (Line3 z in x.lines)
                                                                {
                                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            break;
                                                        case ConsoleKey.Escape:
                                                            act = false;
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (commandd[5] == "local")
                                    {
                                        foreach (Object x in objects)
                                        {
                                            if (x.name == commandd[1])
                                            {
                                                render.ClearScreen();
                                                x.rotateLocal(new Vector3(-Convert.ToDouble(commandd[2]), -Convert.ToDouble(commandd[3]), -Convert.ToDouble(commandd[4])));
                                                foreach (Line3 z in x.lines)
                                                {
                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                }
                                                render.renderFastAsFuck();

                                            }
                                        }
                                    }
                                }
                                else if (commandd.Length == 9)
                                {
                                    if (commandd[5] == "around")
                                    {
                                        foreach (Object x in objects)
                                        {
                                            if (x.name == commandd[1])
                                            {
                                                render.ClearScreen();
                                                x.rotateAround(new Vector3(-Convert.ToDouble(commandd[2]), -Convert.ToDouble(commandd[3]), -Convert.ToDouble(commandd[4])), new Vector3(Convert.ToDouble(commandd[6]), Convert.ToDouble(commandd[7]), Convert.ToDouble(commandd[8])));
                                                foreach (Line3 z in x.lines)
                                                {
                                                    render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                                }
                                                render.renderFastAsFuck();

                                            }
                                        }
                                    }
                                }
                                else if (commandd.Length == 5)
                                {
                                    foreach (Object x in objects)
                                    {
                                        if (x.name == commandd[1])
                                        {
                                            render.ClearScreen();
                                            x.rotate(new Vector3(Convert.ToDouble(commandd[2]), Convert.ToDouble(commandd[3]), Convert.ToDouble(commandd[4])));
                                            foreach (Line3 z in x.lines)
                                            {
                                                render.plotLine(new Line2(z.p1.convertVectorTo2D(), z.p2.convertVectorTo2D()));
                                            }
                                            render.renderFastAsFuck();
                                        }
                                    }
                                }
                            }
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

            static void startrnd()
        {
            for (; ; )
            {
                foreach (Line3 x in objects[0].lines)
                {
                    render.plotLine(new Line2(x.p1.convertVectorTo2D(), x.p2.convertVectorTo2D()));
                }
                render.renderFastAsFuckFrame();
                
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
