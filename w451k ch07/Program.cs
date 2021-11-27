using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using w451k_ch07.three_dimension_menagment;

namespace w451k_ch07
{
    public class Program
    {

        static Renderer render;
        static void Main(string[] args)
        {

            render = new Renderer();

            Scene mainScene = new Scene("scene1");

            Camera cam1 = new Camera(new Vector3(0, 0, -100), new Vector3(0, 0, 0), 20, "cam1");

            mainScene.CameraList.Add(cam1);
            Scene.setCurrentScene(mainScene);
            Camera.setCurrentCamera(cam1);

            Object cube = new Object("cube", new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            Object dik = new Object("dik", new Vector3(60, 0, 20), new Vector3(0, 0, 0));
            cube.LoadFromObjFile("C:\\Users\\darre\\source\\repos\\w451k-hu7\\w451k ch07\\monke.obj");
            dik.LoadFromObjFile("C:\\Users\\darre\\source\\repos\\w451k-hu7\\w451k ch07\\duck.obj");
            mainScene.ObjectList.Add(cube);
            mainScene.ObjectList.Add(dik);
            Light light = new Light(new Point3D(new Vector3(20, 0, 0), 20, 0, 0, 51), "light1");
            light.lightDir = new Vector3(0, 0, -1);

            mainScene.LightList.Add(light);
            render.scale = 1;
            Scene.setCurrentScene(mainScene);
            cube.getProjectedFaces();

            Thread rend = new Thread(startrnd);
            List<Triangle3> toprojectL = new List<Triangle3>();
            foreach (Object x in Scene.currentScene.ObjectList)
            {
                x.calculateLight();
                toprojectL.AddRange(x.getProjectedFaces());

            }
            Triangle3[] toprojectA = toprojectL.ToArray();
            Math3D.timSort(ref toprojectA, toprojectA.Length);
            toprojectA.Reverse();
            foreach (Triangle3 y in toprojectA.ToList())
            {


                render.FillTriangle(
                    y.p1.projectSimple(),
                    y.p2.projectSimple(),
                    y.p3.projectSimple()
                    , y.sym);

            }
            render.renderFastAsFuck();
            rend.Start();
            for (; ; )
            {

                cube.calculateLight();
                cube.rotate(new Vector3(0.01, 0.01, 0.01));
                dik.rotate(new Vector3(-0.01, -0.01, -0.01));
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
                                foreach(Object x in Scene.currentScene.ObjectList)
                                {
                                    Console.WriteLine(x.name + "      position: ( x: " + x.location.x + " y: " + x.location.y + " z: " + x.location.z + " )");
                                }
                            }
                        }
                        break;
                    case "transform":
                        if(commandd.Length == 5)
                        {
                            foreach(Object x in Scene.currentScene.ObjectList)
                            {
                                if(x.name == commandd[1])
                                {
                                    render.ClearScreen();
                                    x.transformGlobal(new Vector3(-Convert.ToDouble(commandd[2]), -Convert.ToDouble(commandd[3]), -Convert.ToDouble(commandd[4])));
                                    foreach (Triangle3 y in x.getProjectedFaces())
                                    {
                                        foreach(Line3 z in y.lines)
                                        {
                                            render.plotLine(new Line2(z.p1.projectSimple(), z.p2.projectSimple()));
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
                if (render.wireframe)
                {
                    foreach (Object x in Scene.currentScene.ObjectList)
                    {
                        foreach (Triangle3 y in x.triangles)
                        {



                            foreach (Line3 z in y.lines)
                            {

                                render.plotLine(new Line2(z.p1.projectSimple(), z.p2.projectSimple()));
                            }
                        }
                    }
                    render.renderFastAsFuckFrame();
                }
                else
                {
                    List<Triangle3> toprojectL = new List<Triangle3>();
                    foreach (Object x in Scene.currentScene.ObjectList)
                    {
                        x.calculateLight();
                        toprojectL.AddRange(x.getProjectedFaces());

                    }
                    Triangle3[] toprojectA = toprojectL.ToArray();
                    Math3D.timSort(ref toprojectA, toprojectA.Length);
                    toprojectL = toprojectA.ToList();
                    toprojectL.Reverse();

                    foreach (Triangle3 y in toprojectL)
                    {


                        render.FillTriangle(
                            y.p1.projectSimple(),
                            y.p2.projectSimple(),
                            y.p3.projectSimple()
                            , y.sym);

                    }
                    render.renderFastAsFuckFrame();
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
