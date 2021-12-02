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

            Camera cam = new Camera(new Vector3(0, 0, -100), new Vector3(0, 0, 0), 20, "cam");

            mainScene.CameraList.Add(cam);
            Scene.setCurrentScene(mainScene);
            Camera.setCurrentCamera(cam);

            Object tytul = new Object("tytul", new Vector3(0, 0, 0), new Vector3(0, 0, 0));

            mainScene.ObjectList.Add(tytul);

            
            tytul.LoadFromObjFile("C:\\Users\\darre\\source\\repos\\w451k-hu7\\w451k ch07\\duck.obj");

            tytul.rotate(new Vector3(0, 0.3, 0));

            Light light = new Light(new Point3D(new Vector3(20, 0, 0), 20, 0, 0, 51), "light1");
            light.lightDir = new Vector3(0, 0, -1);
            mainScene.LightList.Add(light);
            render.scale = 1;
            Scene.setCurrentScene(mainScene);


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

            Scene.currentScene.ObjectList.Remove(tytul);
            /*
            rend.Start();
            
            
            for (; ; )
            {
                cube.calculateLight();
                cube.rotate(new Vector3(0.01, 0.01, 0.01));
                dik.rotate(new Vector3(-0.01, -0.01, -0.01));
                Thread.Sleep(14);
            }
            */


            for (; ; )
            {
                try
                {
                    string command = Console.ReadLine();
                    string[] commandd = command.Split(' ');
                    Console.Clear();
                    switch (commandd[0])
                    {
                        case "wireframe":
                            {
                                if(render.wireframe == false)
                                {
                                    render.wireframe = true;
                                    Console.Clear();
                                    Console.WriteLine("Wireframe on");
                                }else
                                {
                                    render.wireframe = false;
                                    Console.Clear();
                                    Console.WriteLine("Wireframe off");
                                }
                            }
                            break;
                        case "help":
                            {
                                Console.WriteLine("load <object> - wczytaj plik .obj do obiektu \ncreate camera <x y z> <x y z> <odleglosc> <nazwa> - tworzy kamere o wektorze polozenia xyz i wektorze rotacji xyz\n create scene nazwa - tworzy scene o podanej nazwie\n create light x y z x y z nazwa - tworzy swiatlo\n add x y z x y z id to <object> - dodaje point3d do obiektu\n connect id id <object> - laczy dwa Point3D nalezace do danego obiektu\n point - mazak\n show objects - wypisuje obiekty\n show cameras - wypisuje kamery\n show lights - wypisuje swiatla\n delete camera <name> - usuwa kamere\n delete scene - usuwa scene\n delete object <name> - usuwa obiekt\n detele light <name> - usuwa swiatlo\n transform camera <name> x y z - zmienia polozenie kamery o wektor x y z\n transform light <name> x y z - zmienia polozenie swiatla o wektor x y z\n transform <object> x y z - zmeinia polozenie obiektu o wektor x y z\n transform <object> - pozwala zmienic polozenie obiektu za pomoca WSAD, ENTER i BACKSPACE\n render - renderuje\n rotate animate <object> <speed> - nieskonczona rotacja obiektu o okreslonej predkosci\n rotate transform <object> - rotowanie i zmienianie polozenia obieku za pomoca wsad i strzalek jednoczesnie\n rotate <object> local - rotowanie obiektu lokalnie\n rotate object <object> around x y z - pozwala obracac obiekt dookola wektora x y z\n rotate <object> x y z local - obraca obiekt o x y z lokalnie\n set camera <name> - pozwala ustawic kamere ktora ma byc uzywana ");
                            }
                            break;
                        case "set":
                            {
                                if (commandd[1] == "camera")
                                {
                                    foreach (Camera x in Scene.currentScene.CameraList)
                                    {
                                        if (x.name == commandd[2])
                                        {
                                            Camera.setCurrentCamera(x);
                                            Console.WriteLine("The camera of name " + x.name + " has been set.");
                                        }
                                    }
                                }
                            }
                            break;
                        case "load":
                            {
                                bool loaded = false;
                                if (commandd.Length == 2)
                                {
                                    Console.WriteLine("Provide path of the .obj file:");
                                    string path = Console.ReadLine();
                                    foreach (Object x in Scene.currentScene.ObjectList)
                                    {
                                        if (x.name == commandd[1])
                                        {
                                            x.LoadFromObjFile(path);
                                            loaded = true;
                                            x.calculateLight();
                                            Console.WriteLine("The object has been loaded successfully");
                                            Console.ReadKey();
                                        }
                                    }
                                    if (loaded == false)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Couldn't find the object of provided name");
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("No such command");
                                }
                            }
                            break;
                        case "create":
                            if (commandd[1] == "camera")
                            {
                                if (commandd.Length == 10)
                                {
                                    Camera cam = new Camera(new Vector3(double.Parse(commandd[2]), double.Parse(commandd[3]), double.Parse(commandd[4])), new Vector3(double.Parse(commandd[5]), double.Parse(commandd[6]), double.Parse(commandd[7])), double.Parse(commandd[8]), commandd[9]);
                                    Scene.currentScene.CameraList.Add(cam);
                                    Console.WriteLine("The camera has been created successfully");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("No such command");
                                }
                            }
                            else if (commandd[1] == "scene")
                            {
                                if (commandd.Length == 3)
                                {
                                    Scene scene = new Scene(commandd[2]);
                                    Scene.setCurrentScene(scene);
                                    Console.WriteLine("The scene has been created successfully");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("No such command");
                                }
                            }
                            else if (commandd[1] == "object")
                            {
                                if (commandd.Length == 9)
                                {
                                    Object @object = new Object(commandd[2], new Vector3(double.Parse(commandd[3]), double.Parse(commandd[4]), double.Parse(commandd[5])), new Vector3(double.Parse(commandd[6]), double.Parse(commandd[7]), double.Parse(commandd[8])));
                                    Scene.currentScene.ObjectList.Add(@object);
                                    Console.WriteLine("The object has been created successfully");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("No such command");
                                }
                            }
                            else if (commandd[1] == "light")
                            {
                                if (commandd.Length == 9)
                                {
                                    Light light1 = new Light(new Point3D(new Vector3(double.Parse(commandd[2]), double.Parse(commandd[3]), double.Parse(commandd[4])), double.Parse(commandd[5]), double.Parse(commandd[6]), double.Parse(commandd[7])), commandd[8]);
                                    Scene.currentScene.LightList.Add(light1);
                                    Console.WriteLine("The light has been created successfully");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("no such command");
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("no such command");
                            }
                            break;
                        case "add":
                            if (commandd.Length == 10)
                            {
                                foreach (Object x in Scene.currentScene.ObjectList)
                                {
                                    if (x.name == commandd[9] && commandd[8] == "to")
                                    {
                                        x.addVert(new Point3D(new Vector3(double.Parse(commandd[1]), double.Parse(commandd[2]), double.Parse(commandd[3])), double.Parse(commandd[4]), double.Parse(commandd[5]), double.Parse(commandd[6]), int.Parse(commandd[7])));
                                        Console.Clear();
                                        Console.WriteLine("The point has been added successfully to the object");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid data");
                                    }
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("no such command");
                            }
                            break;
                        case "connect":
                            if (commandd.Length == 5)
                                foreach (Object x in Scene.currentScene.ObjectList)
                                {
                                    if (x.name == commandd[3])
                                    {
                                        x.connectVerts(new int[] { int.Parse(commandd[1]), int.Parse(commandd[2]), int.Parse(commandd[3])});
                                        Console.WriteLine("Verts have been connected successfully");
                                        Console.ReadKey();
                                    }
                                }
                            break;
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
                                    foreach (Object x in Scene.currentScene.ObjectList)
                                    {
                                        Console.WriteLine(x.name + "      position: ( x: " + x.location.x + " y: " + x.location.y + " z: " + x.location.z + " )");
                                    }
                                }
                                else if (commandd[1] == "cameras")
                                {
                                    foreach (Camera x in Scene.currentScene.CameraList)
                                    {
                                        Console.WriteLine(x.name + "      position: ( x: " + x.cameraPosition.x + " y: " + x.cameraPosition.y + " z: " + x.cameraPosition.z + " )");
                                    }
                                }
                                else if (commandd[1] == "lights")
                                {
                                    foreach (Light x in Scene.currentScene.LightList)
                                    {
                                        Console.WriteLine(x.name + "     global position: ( x: " + x.location.global.x + " y: " + x.location.global.y + " z: " + x.location.global.z + " )");
                                        Console.WriteLine(x.name + "     local position: ( x: " + x.location.local.x + " y: " + x.location.local.y + " z: " + x.location.local.z + " )");
                                    }
                                }
                            }
                            break;
                        case "delete":
                            if (commandd[1] == "camera" && commandd.Length == 3)
                            {
                                foreach (Camera c in Scene.currentScene.CameraList)
                                {
                                    if (c.name == commandd[2])
                                    {
                                        Scene.currentScene.CameraList.Remove(c);
                                        Console.WriteLine("The camera has been deleted successfully");
                                        Console.ReadKey();
                                    }
                                }
                            }
                            else if (commandd[1] == "scene")
                            {
                                Scene.currentScene = new Scene("default");
                                Console.WriteLine("The scene has been deleted successfully");
                                Console.ReadKey();
                            }
                            else if (commandd[1] == "object" && commandd.Length == 3)
                            {
                                foreach (Object x in Scene.currentScene.ObjectList)
                                {
                                    if (x.name == commandd[2])
                                    {
                                        Scene.currentScene.ObjectList.Remove(x);
                                        Console.WriteLine("The object has been deleted successfully");
                                        Console.ReadKey();
                                    }
                                }
                            }
                            else if (commandd[1] == "light" && commandd.Length == 3)
                            {
                                foreach (Light l in Scene.currentScene.LightList)
                                {
                                    if (l.name == commandd[2])
                                    {
                                        Scene.currentScene.LightList.Remove(l);
                                        Console.WriteLine("The light has been deleted successfully");
                                        Console.ReadKey();
                                    }
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("no such command");
                            }
                            break;
                        case "transform":
                            if (commandd.Length == 6)
                            {
                                if (commandd[1] == "camera")
                                {

                                    foreach (Camera c in Scene.currentScene.CameraList)
                                    {
                                        if (c.name == commandd[2])
                                        {
                                            c.cameraPosition.x += double.Parse(commandd[3]);
                                            c.cameraPosition.y += double.Parse(commandd[4]);
                                            c.cameraPosition.z += double.Parse(commandd[5]);
                                            Console.WriteLine("Done");
                                            Console.ReadKey();
                                        }
                                    }
                                }

                                else if (commandd[1] == "light")
                                {

                                    foreach (Light l in Scene.currentScene.LightList)
                                    {
                                        if (l.name == commandd[2])
                                        {
                                            l.location.local.x += double.Parse(commandd[3]);
                                            l.location.local.y += double.Parse(commandd[4]);
                                            l.location.local.z += double.Parse(commandd[5]);
                                            Console.WriteLine("Done");
                                            Console.ReadKey();
                                        }
                                    }

                                }
                            }
                            else if (commandd.Length == 5)
                            {
                                foreach (Object x in Scene.currentScene.ObjectList)
                                {
                                    if (x.name == commandd[1])
                                    {
                                        x.transformGlobal(new Vector3(Convert.ToDouble(commandd[2]), Convert.ToDouble(commandd[3]), Convert.ToDouble(commandd[4])));
                                        Console.WriteLine("Done");
                                    }
                                    else
                                    {
                                        render.renderFastAsFuck();
                                    }

                                }

                            }
                            else if (commandd.Length == 2)
                            {
                                bool active = true;
                                while (active == true)
                                {
                                    var key = Console.ReadKey(false).Key;
                                    switch (key)
                                    {
                                        case ConsoleKey.S:
                                            foreach (Object x in Scene.currentScene.ObjectList)
                                            {
                                                if (x.name == commandd[1])
                                                {
                                                    render.ClearScreen();

                                                    if (render.wireframe)
                                                    {
                                                        x.transformGlobal(new Vector3(-1, 0, 0));
                                                        foreach (Object z in Scene.currentScene.ObjectList)
                                                        {
                                                            foreach (Triangle3 y in z.triangles)
                                                            {
                                                                foreach (Line3 d in y.lines)
                                                                {

                                                                    render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                }
                                                            }
                                                        }
                                                        render.renderFastAsFuck();
                                                    }
                                                    else
                                                    {
                                                        x.transformGlobal(new Vector3(0, -1, 0));
                                                        List<Triangle3> toprojectB = new List<Triangle3>();
                                                        foreach (Object o in Scene.currentScene.ObjectList)
                                                        {
                                                            o.calculateLight();
                                                            toprojectL.AddRange(o.getProjectedFaces());

                                                        }
                                                        Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                        render.renderFastAsFuck();
                                                    }

                                                }
                                            }
                                            break;
                                        case ConsoleKey.D:
                                            foreach (Object x in Scene.currentScene.ObjectList)
                                            {
                                                if (x.name == commandd[1])
                                                {
                                                    render.ClearScreen();

                                                    if (render.wireframe)
                                                    {
                                                        x.transformGlobal(new Vector3(0, 1, 0));
                                                        foreach (Object z in Scene.currentScene.ObjectList)
                                                        {
                                                            foreach (Triangle3 y in z.triangles)
                                                            {
                                                                foreach (Line3 d in y.lines)
                                                                {
                                                                    render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                }
                                                            }
                                                        }
                                                        render.renderFastAsFuck();
                                                    }
                                                    else
                                                    {
                                                        x.transformGlobal(new Vector3(1, 0, 0));
                                                        List<Triangle3> toprojectB = new List<Triangle3>();
                                                        foreach (Object o in Scene.currentScene.ObjectList)
                                                        {
                                                            o.calculateLight();
                                                            toprojectL.AddRange(o.getProjectedFaces());

                                                        }
                                                        Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                        render.renderFastAsFuck();
                                                    }

                                                }
                                            }
                                            break;
                                        case ConsoleKey.W:
                                            foreach (Object x in Scene.currentScene.ObjectList)
                                            {
                                                if (x.name == commandd[1])
                                                {
                                                    render.ClearScreen();

                                                    if (render.wireframe)
                                                    {
                                                        x.transformGlobal(new Vector3(1, 0, 0));
                                                        foreach (Object z in Scene.currentScene.ObjectList)
                                                        {
                                                            foreach (Triangle3 y in z.triangles)
                                                            {
                                                                foreach (Line3 d in y.lines)
                                                                {
                                                                    render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                }
                                                            }
                                                        }
                                                        render.renderFastAsFuck();
                                                    }
                                                    else
                                                    {
                                                        x.transformGlobal(new Vector3(0, 1, 0));
                                                        List<Triangle3> toprojectB = new List<Triangle3>();
                                                        foreach (Object o in Scene.currentScene.ObjectList)
                                                        {
                                                            if (o.name == commandd[1])
                                                            {
                                                                o.calculateLight();
                                                                toprojectL.AddRange(o.getProjectedFaces());
                                                            }
                                                        }
                                                        Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                        render.renderFastAsFuck();
                                                    }

                                                }
                                            }
                                            break;
                                        case ConsoleKey.A:
                                            foreach (Object x in Scene.currentScene.ObjectList)
                                            {
                                                if (x.name == commandd[1])
                                                {
                                                    render.ClearScreen();

                                                    if (render.wireframe)
                                                    {
                                                        x.transformGlobal(new Vector3(0, -1, 0));
                                                        foreach (Object z in Scene.currentScene.ObjectList)
                                                        {
                                                            foreach (Triangle3 y in z.triangles)
                                                            {
                                                                foreach (Line3 d in y.lines)
                                                                {

                                                                    render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                }
                                                            }
                                                        }
                                                        render.renderFastAsFuck();
                                                    }
                                                    else
                                                    {
                                                        x.transformGlobal(new Vector3(-1, 0, 0));
                                                        List<Triangle3> toprojectB = new List<Triangle3>();
                                                        foreach (Object o in Scene.currentScene.ObjectList)
                                                        {
                                                            o.calculateLight();
                                                            toprojectL.AddRange(o.getProjectedFaces());

                                                        }
                                                        Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                        render.renderFastAsFuck();
                                                    }

                                                }
                                            }
                                            break;
                                        case ConsoleKey.Enter:
                                            foreach (Object x in Scene.currentScene.ObjectList)
                                            {
                                                if (x.name == commandd[1])
                                                {
                                                    render.ClearScreen();

                                                    if (render.wireframe)
                                                    {
                                                        x.transformGlobal(new Vector3(0, 0, 1));
                                                        foreach (Object z in Scene.currentScene.ObjectList)
                                                        {
                                                            foreach (Triangle3 y in z.triangles)
                                                            {
                                                                foreach (Line3 d in y.lines)
                                                                {

                                                                    render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                }
                                                            }
                                                        }
                                                        render.renderFastAsFuck();
                                                    }
                                                    else
                                                    {
                                                        x.transformGlobal(new Vector3(0, 0, 1));
                                                        List<Triangle3> toprojectB = new List<Triangle3>();
                                                        foreach (Object o in Scene.currentScene.ObjectList)
                                                        {
                                                            o.calculateLight();
                                                            toprojectL.AddRange(o.getProjectedFaces());

                                                        }
                                                        Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                        render.renderFastAsFuck();
                                                    }

                                                }
                                            }
                                            break;
                                        case ConsoleKey.Backspace:
                                            foreach (Object x in Scene.currentScene.ObjectList)
                                            {
                                                if (x.name == commandd[1])
                                                {
                                                    render.ClearScreen();

                                                    if (render.wireframe)
                                                    {
                                                        x.transformGlobal(new Vector3(0, 0, -1));
                                                        foreach (Object z in Scene.currentScene.ObjectList)
                                                        {
                                                            foreach (Triangle3 y in z.triangles)
                                                            {
                                                                foreach (Line3 d in y.lines)
                                                                {

                                                                    render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                }
                                                            }
                                                        }
                                                        render.renderFastAsFuck();
                                                    }
                                                    else
                                                    {
                                                        x.transformGlobal(new Vector3(0, 0, -1));
                                                        List<Triangle3> toprojectB = new List<Triangle3>();
                                                        foreach (Object o in Scene.currentScene.ObjectList)
                                                        {
                                                            o.calculateLight();
                                                            toprojectL.AddRange(o.getProjectedFaces());

                                                        }
                                                        Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                        render.renderFastAsFuck();
                                                    }

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
                                if (render.wireframe)
                                {
                                    foreach (Object z in Scene.currentScene.ObjectList)
                                    {
                                        foreach (Triangle3 y in z.triangles)
                                        {
                                            foreach (Line3 d in y.lines)
                                            {

                                                render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                            }
                                        }
                                    }
                                    render.renderFastAsFuck();
                                }
                                else
                                {
                                    List<Triangle3> toprojectB = new List<Triangle3>();
                                    foreach (Object o in Scene.currentScene.ObjectList)
                                    {
                                        o.calculateLight();
                                        toprojectL.AddRange(o.getProjectedFaces());

                                    }
                                    Triangle3[] toprojectC = toprojectL.ToArray();
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
                                    render.renderFastAsFuck();
                                }
                            }
                            break;
                        case "rotate":
                            {
                                if (commandd.Length == 7)
                                {
                                    foreach (Object x in Scene.currentScene.ObjectList)
                                    {
                                        if (x.name == commandd[2])
                                        {
                                            Vector3 point = new Vector3(-Convert.ToDouble(commandd[4]), -Convert.ToDouble(commandd[5]), -Convert.ToDouble(commandd[6]));
                                            bool act = true;
                                            while (act)
                                            {
                                                var key = Console.ReadKey(false).Key;
                                                switch (key)
                                                {
                                                    case ConsoleKey.DownArrow:
                                                        {
                                                            render.ClearScreen();

                                                            if (render.wireframe)
                                                            {
                                                                x.rotateAround(new Vector3(0, -0.05, 0), point);
                                                                foreach (Object z in Scene.currentScene.ObjectList)
                                                                {
                                                                    foreach (Triangle3 y in z.triangles)
                                                                    {
                                                                        foreach (Line3 d in y.lines)
                                                                        {

                                                                            render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                        }
                                                                    }
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            else
                                                            {
                                                                x.rotateAround(new Vector3(-0.05, 0, 0), new Vector3(-point.x, -point.y, -point.z));
                                                                List<Triangle3> toprojectB = new List<Triangle3>();
                                                                foreach (Object o in Scene.currentScene.ObjectList)
                                                                {
                                                                    o.calculateLight();
                                                                    toprojectL.AddRange(o.getProjectedFaces());

                                                                }
                                                                Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                render.renderFastAsFuck();
                                                            }
                                                        }
                                                        break;
                                                    case ConsoleKey.UpArrow:
                                                        {
                                                            render.ClearScreen();

                                                            if (render.wireframe)
                                                            {
                                                                x.rotateAround(new Vector3(0, 0.05, 0), point);
                                                                foreach (Object z in Scene.currentScene.ObjectList)
                                                                {
                                                                    foreach (Triangle3 y in z.triangles)
                                                                    {
                                                                        foreach (Line3 d in y.lines)
                                                                        {

                                                                            render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                        }
                                                                    }
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            else
                                                            {
                                                                x.rotateAround(new Vector3(0.05, 0, 0), point);
                                                                List<Triangle3> toprojectB = new List<Triangle3>();
                                                                foreach (Object o in Scene.currentScene.ObjectList)
                                                                {
                                                                    o.calculateLight();
                                                                    toprojectL.AddRange(o.getProjectedFaces());

                                                                }
                                                                Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                render.renderFastAsFuck();
                                                            }
                                                        }
                                                        break;
                                                    case ConsoleKey.RightArrow:
                                                        {
                                                            render.ClearScreen();

                                                            if (render.wireframe)
                                                            {
                                                                x.rotateAround(new Vector3(-0.05, 0, 0), point);

                                                                foreach (Object z in Scene.currentScene.ObjectList)
                                                                {
                                                                    foreach (Triangle3 y in z.triangles)
                                                                    {
                                                                        foreach (Line3 d in y.lines)
                                                                        {

                                                                            render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                        }
                                                                    }
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            else
                                                            {
                                                                x.rotateAround(new Vector3(0, -0.05, 0), point);
                                                                List<Triangle3> toprojectB = new List<Triangle3>();
                                                                foreach (Object o in Scene.currentScene.ObjectList)
                                                                {
                                                                    o.calculateLight();
                                                                    toprojectL.AddRange(o.getProjectedFaces());

                                                                }
                                                                Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                render.renderFastAsFuck();
                                                            }
                                                        }
                                                        break;
                                                    case ConsoleKey.LeftArrow:
                                                        {
                                                            render.ClearScreen();

                                                            if (render.wireframe)
                                                            {
                                                                x.rotateAround(new Vector3(0.05, 0, 0), point);
                                                                foreach (Object z in Scene.currentScene.ObjectList)
                                                                {
                                                                    foreach (Triangle3 y in z.triangles)
                                                                    {
                                                                        foreach (Line3 d in y.lines)
                                                                        {

                                                                            render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                        }
                                                                    }
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            else
                                                            {
                                                                x.rotateAround(new Vector3(0, 0.05, 0), point);
                                                                List<Triangle3> toprojectB = new List<Triangle3>();
                                                                foreach (Object o in Scene.currentScene.ObjectList)
                                                                {
                                                                    o.calculateLight();
                                                                    toprojectL.AddRange(o.getProjectedFaces());

                                                                }
                                                                Triangle3[] toprojectC = toprojectL.ToArray();
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
                                    }
                                }
                                else if (commandd[1] == "animate")
                                {   
                                    if(commandd.Length == 5)
                                    {
                                        if(commandd[2] == "x")
                                        {
                                            foreach (Object x in Scene.currentScene.ObjectList)
                                            {
                                                if (x.name == commandd[3])
                                                {
                                                    Thread rend1 = new Thread(startrnd);
                                                    rend1.Start();
                                                    while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                                    {
                                                        x.calculateLight();
                                                        x.rotateX(Double.Parse(commandd[4]));
                                                        Thread.Sleep(14);
                                                    }
                                                    rend1.Abort();
                                                }
                                            }
                                        }
                                        else if(commandd[2] == "y")
                                        {
                                            foreach (Object x in Scene.currentScene.ObjectList)
                                            {
                                                if (x.name == commandd[3])
                                                {
                                                    Thread rend1 = new Thread(startrnd);
                                                    rend1.Start();
                                                    while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                                    {
                                                        x.calculateLight();
                                                        x.rotateY(Double.Parse(commandd[4]));
                                                        Thread.Sleep(14);
                                                    }
                                                    rend1.Abort();
                                                }
                                            }
                                        }
                                        else if(commandd[2] == "z")
                                        {
                                            foreach (Object x in Scene.currentScene.ObjectList)
                                            {
                                                if (x.name == commandd[3])
                                                {
                                                    Thread rend1 = new Thread(startrnd);
                                                    rend1.Start();
                                                    while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                                    {
                                                        x.calculateLight();
                                                        x.rotateZ(Double.Parse(commandd[4]));
                                                        Thread.Sleep(14);
                                                    }
                                                    rend1.Abort();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("no such command");
                                        }
                                    }
                                    else if (commandd.Length == 4)
                                    {
                                        foreach (Object x in Scene.currentScene.ObjectList)
                                        {
                                            if (x.name == commandd[2])
                                            {
                                                Thread rend1 = new Thread(startrnd);
                                                rend1.Start();
                                                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                                                {
                                                    x.calculateLight();
                                                    x.rotate(new Vector3(double.Parse(commandd[3]), double.Parse(commandd[3]), double.Parse(commandd[3])));
                                                    Thread.Sleep(14);
                                                }
                                                rend1.Abort();
                                            }
                                        }
                                    }

                                }

                                else if (commandd.Length == 3)
                                {
                                    if (commandd[1] == "transform")
                                    {
                                        bool act = true;
                                        while (act)
                                        {
                                            var key = Console.ReadKey(false).Key;
                                            switch (key)
                                            {
                                                case ConsoleKey.UpArrow:
                                                    {
                                                        foreach (Object x in Scene.currentScene.ObjectList)
                                                        {
                                                            if (x.name == commandd[2])
                                                            {
                                                                render.ClearScreen();

                                                                if (render.wireframe)
                                                                {
                                                                    x.rotateY(0.05);
                                                                    foreach (Object z in Scene.currentScene.ObjectList)
                                                                    {
                                                                        foreach (Triangle3 y in z.triangles)
                                                                        {
                                                                            foreach (Line3 d in y.lines)
                                                                            {

                                                                                render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                            }
                                                                        }
                                                                    }
                                                                    render.renderFastAsFuck();
                                                                }
                                                                else
                                                                {
                                                                    x.rotateX(0.05);
                                                                    List<Triangle3> toprojectB = new List<Triangle3>();
                                                                    foreach (Object o in Scene.currentScene.ObjectList)
                                                                    {
                                                                        o.calculateLight();
                                                                        toprojectL.AddRange(o.getProjectedFaces());

                                                                    }
                                                                    Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                    render.renderFastAsFuck();
                                                                }

                                                            }
                                                        }
                                                    }
                                                    break;
                                                case ConsoleKey.DownArrow:
                                                    {
                                                        foreach (Object x in Scene.currentScene.ObjectList)
                                                        {
                                                            if (x.name == commandd[2])
                                                            {
                                                                render.ClearScreen();

                                                                if (render.wireframe)
                                                                {
                                                                    x.rotateY(-0.05);
                                                                    foreach (Object z in Scene.currentScene.ObjectList)
                                                                    {
                                                                        foreach (Triangle3 y in z.triangles)
                                                                        {
                                                                            foreach (Line3 d in y.lines)
                                                                            {

                                                                                render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                            }
                                                                        }
                                                                    }
                                                                    render.renderFastAsFuck();
                                                                }
                                                                else
                                                                {
                                                                    x.rotateX(-0.05);
                                                                    List<Triangle3> toprojectB = new List<Triangle3>();
                                                                    foreach (Object o in Scene.currentScene.ObjectList)
                                                                    {
                                                                        o.calculateLight();
                                                                        toprojectL.AddRange(o.getProjectedFaces());

                                                                    }
                                                                    Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                    render.renderFastAsFuck();
                                                                }

                                                            }
                                                        }
                                                    }
                                                    break;
                                                case ConsoleKey.RightArrow:
                                                    {
                                                        foreach (Object x in Scene.currentScene.ObjectList)
                                                        {
                                                            if (x.name == commandd[2])
                                                            {
                                                                render.ClearScreen();

                                                                if (render.wireframe)
                                                                {
                                                                    x.rotateX(-0.05);
                                                                    foreach (Object z in Scene.currentScene.ObjectList)
                                                                    {
                                                                        foreach (Triangle3 y in z.triangles)
                                                                        {
                                                                            foreach (Line3 d in y.lines)
                                                                            {

                                                                                render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                            }
                                                                        }
                                                                    }
                                                                    render.renderFastAsFuck();
                                                                }
                                                                else
                                                                {
                                                                    x.rotateY(-0.05);
                                                                    List<Triangle3> toprojectB = new List<Triangle3>();
                                                                    foreach (Object o in Scene.currentScene.ObjectList)
                                                                    {
                                                                        o.calculateLight();
                                                                        toprojectL.AddRange(o.getProjectedFaces());

                                                                    }
                                                                    Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                    render.renderFastAsFuck();
                                                                }

                                                            }
                                                        }
                                                    }
                                                    break;
                                                case ConsoleKey.LeftArrow:
                                                    {
                                                        foreach (Object x in Scene.currentScene.ObjectList)
                                                        {
                                                            if (x.name == commandd[2])
                                                            {
                                                                render.ClearScreen();

                                                                if (render.wireframe)
                                                                {
                                                                    x.rotateX(0.05);
                                                                    foreach (Object z in Scene.currentScene.ObjectList)
                                                                    {
                                                                        foreach (Triangle3 y in z.triangles)
                                                                        {
                                                                            foreach (Line3 d in y.lines)
                                                                            {

                                                                                render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                            }
                                                                        }
                                                                    }
                                                                    render.renderFastAsFuck();
                                                                }
                                                                else
                                                                {
                                                                    x.rotateY(0.05);
                                                                    List<Triangle3> toprojectB = new List<Triangle3>();
                                                                    foreach (Object o in Scene.currentScene.ObjectList)
                                                                    {
                                                                        o.calculateLight();
                                                                        toprojectL.AddRange(o.getProjectedFaces());

                                                                    }
                                                                    Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                    render.renderFastAsFuck();
                                                                }

                                                            }
                                                        }
                                                    }
                                                    break;
                                                case ConsoleKey.S:
                                                    foreach (Object x in Scene.currentScene.ObjectList)
                                                    {
                                                        if (x.name == commandd[2])
                                                        {
                                                            render.ClearScreen();

                                                            if (render.wireframe)
                                                            {
                                                                x.transformGlobal(new Vector3(-1, 0, 0));
                                                                foreach (Object z in Scene.currentScene.ObjectList)
                                                                {
                                                                    foreach (Triangle3 y in z.triangles)
                                                                    {
                                                                        foreach (Line3 d in y.lines)
                                                                        {

                                                                            render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                        }
                                                                    }
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            else
                                                            {
                                                                x.transformGlobal(new Vector3(0, -1, 0));
                                                                List<Triangle3> toprojectB = new List<Triangle3>();
                                                                foreach (Object o in Scene.currentScene.ObjectList)
                                                                {
                                                                    o.calculateLight();
                                                                    toprojectL.AddRange(o.getProjectedFaces());

                                                                }
                                                                Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                render.renderFastAsFuck();
                                                            }

                                                        }
                                                    }
                                                    break;
                                                case ConsoleKey.D:
                                                    foreach (Object x in Scene.currentScene.ObjectList)
                                                    {
                                                        if (x.name == commandd[2])
                                                        {
                                                            render.ClearScreen();

                                                            if (render.wireframe)
                                                            {
                                                                x.transformGlobal(new Vector3(0, 1, 0));
                                                                foreach (Object z in Scene.currentScene.ObjectList)
                                                                {
                                                                    foreach (Triangle3 y in z.triangles)
                                                                    {
                                                                        foreach (Line3 d in y.lines)
                                                                        {

                                                                            render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                        }
                                                                    }
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            else
                                                            {
                                                                x.transformGlobal(new Vector3(1, 0, 0));
                                                                List<Triangle3> toprojectB = new List<Triangle3>();
                                                                foreach (Object o in Scene.currentScene.ObjectList)
                                                                {
                                                                    o.calculateLight();
                                                                    toprojectL.AddRange(o.getProjectedFaces());

                                                                }
                                                                Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                render.renderFastAsFuck();
                                                            }

                                                        }
                                                    }
                                                    break;
                                                case ConsoleKey.W:
                                                    foreach (Object x in Scene.currentScene.ObjectList)
                                                    {
                                                        if (x.name == commandd[2])
                                                        {
                                                            render.ClearScreen();

                                                            if (render.wireframe)
                                                            {
                                                                x.transformGlobal(new Vector3(1, 0, 0));
                                                                foreach (Object z in Scene.currentScene.ObjectList)
                                                                {
                                                                    foreach (Triangle3 y in z.triangles)
                                                                    {
                                                                        foreach (Line3 d in y.lines)
                                                                        {

                                                                            render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                        }
                                                                    }
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            else
                                                            {
                                                                x.transformGlobal(new Vector3(0, 1, 0));
                                                                List<Triangle3> toprojectB = new List<Triangle3>();
                                                                foreach (Object o in Scene.currentScene.ObjectList)
                                                                {
                                                                    o.calculateLight();
                                                                    toprojectL.AddRange(o.getProjectedFaces());

                                                                }
                                                                Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                render.renderFastAsFuck();
                                                            }

                                                        }
                                                    }
                                                    break;
                                                case ConsoleKey.A:
                                                    foreach (Object x in Scene.currentScene.ObjectList)
                                                    {
                                                        if (x.name == commandd[2])
                                                        {
                                                            render.ClearScreen();

                                                            if (render.wireframe)
                                                            {
                                                                x.transformGlobal(new Vector3(0, -1, 0));
                                                                foreach (Object z in Scene.currentScene.ObjectList)
                                                                {
                                                                    foreach (Triangle3 y in z.triangles)
                                                                    {
                                                                        foreach (Line3 d in y.lines)
                                                                        {

                                                                            render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                        }
                                                                    }
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            else
                                                            {
                                                                x.transformGlobal(new Vector3(-1, 0, 0));
                                                                List<Triangle3> toprojectB = new List<Triangle3>();
                                                                foreach (Object o in Scene.currentScene.ObjectList)
                                                                {
                                                                    o.calculateLight();
                                                                    toprojectL.AddRange(o.getProjectedFaces());

                                                                }
                                                                Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                render.renderFastAsFuck();
                                                            }

                                                        }
                                                    }
                                                    break;
                                                case ConsoleKey.Enter:
                                                    foreach (Object x in Scene.currentScene.ObjectList)
                                                    {
                                                        if (x.name == commandd[2])
                                                        {
                                                            render.ClearScreen();

                                                            if (render.wireframe)
                                                            {
                                                                x.transformGlobal(new Vector3(0, 0, 1));
                                                                foreach (Object z in Scene.currentScene.ObjectList)
                                                                {
                                                                    foreach (Triangle3 y in z.triangles)
                                                                    {
                                                                        foreach (Line3 d in y.lines)
                                                                        {

                                                                            render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                        }
                                                                    }
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            else
                                                            {
                                                                x.transformGlobal(new Vector3(0, 0, 1));
                                                                List<Triangle3> toprojectB = new List<Triangle3>();
                                                                foreach (Object o in Scene.currentScene.ObjectList)
                                                                {
                                                                    o.calculateLight();
                                                                    toprojectL.AddRange(o.getProjectedFaces());

                                                                }
                                                                Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                render.renderFastAsFuck();
                                                            }

                                                        }
                                                    }
                                                    break;
                                                case ConsoleKey.Backspace:
                                                    foreach (Object x in Scene.currentScene.ObjectList)
                                                    {
                                                        if (x.name == commandd[2])
                                                        {
                                                            render.ClearScreen();

                                                            if (render.wireframe)
                                                            {
                                                                x.transformGlobal(new Vector3(0, 0, -1));
                                                                foreach (Object z in Scene.currentScene.ObjectList)
                                                                {
                                                                    foreach (Triangle3 y in z.triangles)
                                                                    {
                                                                        foreach (Line3 d in y.lines)
                                                                        {

                                                                            render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                        }
                                                                    }
                                                                }
                                                                render.renderFastAsFuck();
                                                            }
                                                            else
                                                            {
                                                                x.transformGlobal(new Vector3(0, 0, -1));
                                                                List<Triangle3> toprojectB = new List<Triangle3>();
                                                                foreach (Object o in Scene.currentScene.ObjectList)
                                                                {
                                                                    o.calculateLight();
                                                                    toprojectL.AddRange(o.getProjectedFaces());

                                                                }
                                                                Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                render.renderFastAsFuck();
                                                            }

                                                        }
                                                    }
                                                    break;
                                                case ConsoleKey.Escape:
                                                    act = false;
                                                    break;
                                            }
                                        }

                                    }
                                    if (commandd[2] == "local")
                                    {
                                        foreach (Object x in Scene.currentScene.ObjectList)
                                        {
                                            if (x.name == commandd[1])
                                            {
                                                bool act = true;
                                                while (act)
                                                {
                                                    var key = Console.ReadKey(false).Key;
                                                    switch (key)
                                                    {
                                                        case ConsoleKey.DownArrow:
                                                            {
                                                                render.ClearScreen();

                                                                if (render.wireframe)
                                                                {
                                                                    x.rotateLocal(new Vector3(0, 0.05, 0));
                                                                    foreach (Object z in Scene.currentScene.ObjectList)
                                                                    {
                                                                        foreach (Triangle3 y in z.triangles)
                                                                        {
                                                                            foreach (Line3 d in y.lines)
                                                                            {

                                                                                render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                            }
                                                                        }
                                                                    }
                                                                    render.renderFastAsFuck();
                                                                }
                                                                else
                                                                {
                                                                    x.rotateLocal(new Vector3(-0.05, 0, 0));
                                                                    List<Triangle3> toprojectB = new List<Triangle3>();
                                                                    foreach (Object o in Scene.currentScene.ObjectList)
                                                                    {
                                                                        o.calculateLight();
                                                                        toprojectL.AddRange(o.getProjectedFaces());

                                                                    }
                                                                    Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                    render.renderFastAsFuck();
                                                                }
                                                            }
                                                            break;
                                                        case ConsoleKey.UpArrow:
                                                            {
                                                                render.ClearScreen();

                                                                if (render.wireframe)
                                                                {
                                                                    x.rotateLocal(new Vector3(0, -0.05, 0));
                                                                    foreach (Object z in Scene.currentScene.ObjectList)
                                                                    {
                                                                        foreach (Triangle3 y in z.triangles)
                                                                        {
                                                                            foreach (Line3 d in y.lines)
                                                                            {

                                                                                render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                            }
                                                                        }
                                                                    }
                                                                    render.renderFastAsFuck();
                                                                }
                                                                else
                                                                {
                                                                    x.rotateLocal(new Vector3(0.05, 0, 0));
                                                                    List<Triangle3> toprojectB = new List<Triangle3>();
                                                                    foreach (Object o in Scene.currentScene.ObjectList)
                                                                    {
                                                                        o.calculateLight();
                                                                        toprojectL.AddRange(o.getProjectedFaces());

                                                                    }
                                                                    Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                    render.renderFastAsFuck();
                                                                }
                                                            }
                                                            break;
                                                        case ConsoleKey.RightArrow:
                                                            {
                                                                render.ClearScreen();

                                                                if (render.wireframe)
                                                                {
                                                                    x.rotateLocal(new Vector3(0.05, 0, 0));
                                                                    foreach (Object z in Scene.currentScene.ObjectList)
                                                                    {
                                                                        foreach (Triangle3 y in z.triangles)
                                                                        {
                                                                            foreach (Line3 d in y.lines)
                                                                            {

                                                                                render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                            }
                                                                        }
                                                                    }
                                                                    render.renderFastAsFuck();
                                                                }
                                                                else
                                                                {
                                                                    x.rotateLocal(new Vector3(0, -0.05, 0));
                                                                    List<Triangle3> toprojectB = new List<Triangle3>();
                                                                    foreach (Object o in Scene.currentScene.ObjectList)
                                                                    {
                                                                        o.calculateLight();
                                                                        toprojectL.AddRange(o.getProjectedFaces());

                                                                    }
                                                                    Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                                    render.renderFastAsFuck();
                                                                }
                                                            }
                                                            break;
                                                        case ConsoleKey.LeftArrow:
                                                            {
                                                                render.ClearScreen();

                                                                if (render.wireframe)
                                                                {
                                                                    x.rotateLocal(new Vector3(-0.05, 0, 0));
                                                                    foreach (Object z in Scene.currentScene.ObjectList)
                                                                    {
                                                                        foreach (Triangle3 y in z.triangles)
                                                                        {
                                                                            foreach (Line3 d in y.lines)
                                                                            {

                                                                    render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                                }
                                                            }
                                                        }
                                                        render.renderFastAsFuck();
                                                    }
                                                    else
                                                    {
                                                        x.transformGlobal(new Vector3(0, 0, -1));
                                                        List<Triangle3> toprojectB = new List<Triangle3>();
                                                        foreach (Object o in Scene.currentScene.ObjectList)
                                                        {
                                                            o.calculateLight();
                                                            toprojectL.AddRange(o.getProjectedFaces());

                                                        }
                                                        Triangle3[] toprojectC = toprojectL.ToArray();
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
                                        }
                                    }

                                }
                                else if (commandd[5] == "local")
                                {
                                    foreach (Object x in Scene.currentScene.ObjectList)
                                    {
                                        if (x.name == commandd[1])
                                        {
                                            render.ClearScreen();

                                            if (render.wireframe)
                                                x.rotateLocal(new Vector3(Convert.ToDouble(commandd[2]), Convert.ToDouble(commandd[3]), Convert.ToDouble(commandd[4])));
                                            foreach (Object z in Scene.currentScene.ObjectList)
                                            {
                                                foreach (Triangle3 y in z.triangles)
                                                {
                                                    foreach (Line3 d in y.lines)
                                                    {

                                                        render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                    }
                                                }
                                            }
                                            render.renderFastAsFuck();
                                        }
                                        else
                                        {
                                            x.rotateLocal(new Vector3(Convert.ToDouble(commandd[2]), Convert.ToDouble(commandd[3]), Convert.ToDouble(commandd[4])));
                                            List<Triangle3> toprojectB = new List<Triangle3>();
                                            foreach (Object o in Scene.currentScene.ObjectList)
                                            {
                                                o.calculateLight();
                                                toprojectL.AddRange(o.getProjectedFaces());

                                            }
                                            Triangle3[] toprojectC = toprojectL.ToArray();
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
                                            render.renderFastAsFuck();
                                        }
                                    }

                                }
                                else if (commandd.Length == 5)
                                {
                                    foreach (Object x in Scene.currentScene.ObjectList)
                                    {
                                        if (x.name == commandd[1])
                                        {
                                            render.ClearScreen();

                                            if (render.wireframe)
                                            {
                                                x.rotate(new Vector3(Convert.ToDouble(commandd[2]), Convert.ToDouble(commandd[3]), Convert.ToDouble(commandd[4])));
                                                foreach (Object z in Scene.currentScene.ObjectList)
                                                {
                                                    foreach (Triangle3 y in z.triangles)
                                                    {
                                                        foreach (Line3 d in y.lines)
                                                        {
                                                            render.plotLine(new Line2(d.p1.projectSimple(), d.p2.projectSimple()));
                                                        }
                                                    }
                                                }
                                                render.renderFastAsFuck();
                                            }
                                            else
                                            {
                                                x.rotate(new Vector3(-Convert.ToDouble(commandd[2]), -Convert.ToDouble(commandd[3]), -Convert.ToDouble(commandd[4])));
                                                foreach (Object o in Scene.currentScene.ObjectList)
                                                {
                                                    o.calculateLight();
                                                    toprojectL.AddRange(o.getProjectedFaces());

                                                }
                                                Triangle3[] toprojectC = toprojectL.ToArray();
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
                                                render.renderFastAsFuck();
                                            }
                                        }
                                    }
                                }
                                break;

                            }

                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("no such command");
                            }
                            break;
                    }


                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Try again");
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
