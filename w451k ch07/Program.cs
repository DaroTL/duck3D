using System;
using System.Collections.Generic;
using System.Threading;

namespace w451k_ch07
{
    public class Program
    {
        static List<Object> objects = new List<Object>();
        static Renderer render;
        static Vector3 cameraPosition = new Vector3(0, 0, -60);
        static Vector3 cameraRotation = new Vector3(0, 0, 0);
        static void Main(string[] args)
        {

            render = new Renderer();

            Scene mainScene = new Scene();

            Object cube = new Object("cube", new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            objects.Add(cube);
            cube.LoadFromObjFile("C:\\Users\\darre\\source\\repos\\w451k-hu7\\w451k ch07\\duck.obj");


/*            cube.addVert(new Point3D(cube.location, 0, 0, 0, 100));
            cube.addVert(new Point3D(cube.location, 30, 0, 0, 101));
            cube.addVert(new Point3D(cube.location, 30, 30, 0, 102));
            cube.addVert(new Point3D(cube.location, 0, 30, 0, 103));
            cube.addVert(new Point3D(cube.location, 0, 0, 30, 110));
            cube.addVert(new Point3D(cube.location, 30, 0, 30, 111));
            cube.addVert(new Point3D(cube.location, 30, 30, 30, 112));
            cube.addVert(new Point3D(cube.location, 0, 30, 30, 113));
            cube.connectVerts(new int[] { 100, 103, 102 });
            cube.connectVerts(new int[] { 100, 102, 101 });
            cube.connectVerts(new int[] { 101, 102, 112 });
            cube.connectVerts(new int[] { 101, 112, 111 });
            cube.connectVerts(new int[] { 111, 112, 113 });
            cube.connectVerts(new int[] { 110, 111, 113 });
            cube.connectVerts(new int[] { 110, 113, 103 });
            cube.connectVerts(new int[] { 110, 103, 100 });
            cube.connectVerts(new int[] { 103, 113, 112 });
            cube.connectVerts(new int[] { 103, 112, 102 });
            cube.connectVerts(new int[] { 111, 110, 100 });
            cube.connectVerts(new int[] { 111, 100, 101 });*/


            mainScene.ObjectList.Add(cube);

            Light light = new Light(new Point3D(new Vector3(20, 0, 0), 20, 0, 0, 51));
            light.lightDir = new Vector3(0, 0, -1);

            mainScene.LightList.Add(light); 

            Scene.setCurrentScene(mainScene);


            Thread rend = new Thread(startrnd);

            rend.Start();
            for (; ; )
            {

                cube.calculateLight();
                cube.rotateLocal(new Vector3(0.03, 0, 0));
                Thread.Sleep(14);

            }

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
                            if(commandd[1] == "objects")
                            {
                                foreach(Object x in objects)
                                {
                                    Console.WriteLine(x.name + "      position: ( x: " + x.location.x + " y: " + x.location.y + " z: " + x.location.z + " )");
                                }
                            }
                        }
                        break;
                    case "transform":
                        if(commandd.Length == 5)
                        {
                            foreach(Object x in objects)
                            {
                                if(x.name == commandd[1])
                                {
                                    render.ClearScreen();
                                    x.transformGlobal(new Vector3(-Convert.ToDouble(commandd[2]), -Convert.ToDouble(commandd[3]), -Convert.ToDouble(commandd[4])));
                                    foreach (Triangle3 y in x.getProjectedFaces(cameraPosition,cameraRotation))
                                    {
                                        foreach(Line3 z in y.lines)
                                        {
                                            render.plotLine(new Line2(z.p1.projectSimple(cameraPosition, cameraRotation), z.p2.projectSimple(cameraPosition, cameraRotation)));
                                        }
                                    }
                                    render.renderFastAsFuck();
                                }
                                else
                                {
                                    render.renderFastAsFuck();
                                }

                            }

                        }
                        if(commandd.Length == 2)
                        {

                        }
                        break;
                    case "render":
                        {

                            render.renderFastAsFuck();
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
                foreach (Triangle3 y in objects[0].getProjectedFaces(cameraPosition, cameraRotation))
                {

                    render.FillTriangle(
                        y.p1.projectSimple(cameraPosition, cameraRotation),
                        y.p2.projectSimple(cameraPosition, cameraRotation),
                        y.p3.projectSimple(cameraPosition, cameraRotation)
                        , y.sym);
                    /*foreach (Line3 z in y.lines)
                    {

                        render.plotLine(new Line2(z.p1.projectSimple(cameraPosition, cameraRotation), z.p2.projectSimple(cameraPosition, cameraRotation)));
                    }*/
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
