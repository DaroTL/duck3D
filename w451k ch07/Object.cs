﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace w451k_ch07
{
    public class Object
    {
        public String name = "";
        public Vector3 location = new Vector3(0, 0, 0);
        public Vector3 origin = new Vector3(15, 15, 15);
        Vector3 rotation = new Vector3(0, 0, 0);
        public List<Point3D> verticies = new List<Point3D>();
        public List<Line3> lines = new List<Line3>();
        public List<Triangle3> triangles = new List<Triangle3>();
        public float scale = 2;

        public Object(string _name, Vector3 _location, Vector3 _rotation)
        {
            name = _name;
            location = _location;
            rotation = _rotation;
        }

        public void addVert(Point3D x)
        {
            verticies.Add(x);
        }

        public void connectVerts(int[] x, bool triangle = true)
        {
            if(x.Length > 1)
            {
                
                List<Point3D> toConnect = new List<Point3D>();

                //znajduje po id
                foreach (int c in x)
                {
                    foreach (Point3D y in verticies)
                    {
                    

                        
                        if (y.getid() == c)
                        {
                            Console.WriteLine(y.getid());
                            toConnect.Add(y);
                        }
                    }    

                }
                //laczy 
                
                for (int z = 0; z < toConnect.Count - 1; z++)
                {
                    
                    lines.Add(new Line3(toConnect[z], toConnect[z+1]));
                    if(toConnect.Count == 3 && z+1 == 2)
                    {
                        lines.Add(new Line3(toConnect[toConnect.Count()-1], toConnect[0]));
                        triangles.Add(new Triangle3(toConnect[0], toConnect[1], toConnect[2]));
                        
                    }
                    else
                    {
                        /*                        
                        if (z != 0 && (float)(z + 1) % 3 == 0 && triangle)
                        {

                            lines.Add(new Line3(toConnect[z], toConnect[z - 2]));
                            triangles.Add(new Triangle3(toConnect[z], toConnect[z - 1], toConnect[z - 2]));
                        }
                        if (z + 1 == toConnect.Count - 1)
                        {

                            *//*lines.Add(new Line3(toConnect[0], toConnect[toConnect.Count -1]));*//*
                            triangles.Add(new Triangle3(toConnect[z + 1], toConnect[z], toConnect[0]));

                        }*/
                    }


                }

            }

        }

        public void transformGlobal(Vector3 x)
        {
            location.x = x.x;
            location.y = x.y;
            location.z = x.z;
            foreach ( Point3D z in verticies)
            {
                z.setGlobal(x);
            }
        }








        void rotateX(double rotation)
        {
            double anglecos = Math.Cos(rotation);
            double anglesin = Math.Sin(rotation);
            foreach (Point3D z in verticies)
            {

                
                double newx = z.global.x;
                double newy = (z.global.y * anglecos) - (z.global.z * anglesin);
                double newz = (z.global.z * anglecos) + (z.global.y * anglesin);


                z.global = new Vector3(newx, newy, newz);
            }
        }
        void rotateX(double rotation, Vector3 point)
        {
            double anglecos = Math.Cos(rotation);
            double anglesin = Math.Sin(rotation);
            foreach (Point3D z in verticies)
            {


                double newx = z.global.x - point.x;
                double newy = ((z.global.y - point.y) * anglecos) - ((z.global.z - point.z) * anglesin);
                double newz = ((z.global.z - point.z) * anglecos) + ((z.global.y - point.y) * anglesin);


                z.global = new Vector3(newx + point.x, newy + point.y, newz + point.z);
            }
        }



        void rotateY(double rotation)
        {
            double anglecos = Math.Cos(rotation);
            double anglesin = Math.Sin(rotation);
            foreach (Point3D z in verticies)
            {


                double newx = (z.global.x * anglecos) - (z.global.z * anglesin); 
                double newy = z.global.y;
                double newz = (z.global.z * anglecos) + (z.global.x * anglesin);


                z.global = new Vector3(newx, newy, newz);
            }
        }
        void rotateY(double rotation, Vector3 point)
        {
            double anglecos = Math.Cos(rotation);
            double anglesin = Math.Sin(rotation);
            foreach (Point3D z in verticies)
            {

                double newx = ((z.global.x - point.x) * anglecos) - ((z.global.z - point.z) * anglesin);
                double newy = (z.global.y - point.y);
                double newz = ((z.global.z - point.z) * anglecos) + ((z.global.x - point.x) * anglesin);

                z.global = new Vector3(newx + point.x, newy + point.y, newz + point.z);
            }
        }



        void rotateZ(double rotation)
        {
            double anglecos = Math.Cos(rotation);
            double anglesin = Math.Sin(rotation);
            foreach (Point3D z in verticies)
            {


                double newx = (z.global.x * anglecos) + (z.global.y * anglesin);
                double newy = (z.global.y * anglecos) - (z.global.x * anglesin);
                double newz = z.global.z;


                z.global = new Vector3(newx, newy, newz);
            }
        }
        void rotateZ(double rotation, Vector3 point)
        {
            double anglecos = Math.Cos(rotation);
            double anglesin = Math.Sin(rotation);
            foreach (Point3D z in verticies)
            {

                double newx = ((z.global.x - point.x) * anglecos) + ((z.global.y - point.y) * anglesin);
                double newy = ((z.global.y - point.y) * anglecos) - ((z.global.x - point.x) * anglesin);
                double newz = (z.global.z - point.z);

                z.global = new Vector3(newx + point.x, newy + point.y, newz + point.z);
            }
        }



        public void rotate(Vector3 rotation)
        {
            rotateX(rotation.x);
            rotateY(rotation.y);
            rotateZ(rotation.z);
        }
        public void rotateLocal(Vector3 rotation)
        {
            rotateX(rotation.x, origin);
            rotateY(rotation.y, origin);
            rotateZ(rotation.z, origin);
        }
        public void rotateAround(Vector3 rotation, Vector3 point)
        {
            rotateX(rotation.x, point);
            rotateY(rotation.y, point);
            rotateZ(rotation.z, point);
        }

        public void calculateLight()
        {
            Light[] lights = new Light[Scene.currentScene.LightList.Count()];

            for(int i = 0; i < lights.Length; i++)
            {
                lights[i] = Scene.currentScene.LightList[i];
            }

            Vector3 light = lights[0].lightDir;
            float l = (float)Math.Sqrt(light.x * light.x + light.y * light.y + light.z * light.z);
            light.x /= l;
            light.y /= l;
            light.z /= l;

            for (int i = 0; i < triangles.Count(); i++)
            {
                float dotProduct = 
                    (float)(triangles[i].normalVector.x * light.x +
                    triangles[i].normalVector.y * light.y +
                    triangles[i].normalVector.z * light.z);

                triangles[i].sym = getColor(dotProduct);
            }


        }

        public List<Triangle3> getProjectedFaces(Vector3 cameraPosition, Vector3 cameraRotation)
        {
            List<Triangle3> projected = new List<Triangle3>();

            for(int i = 0; i < triangles.Count(); i++)
            {

                triangles[i].recalculateNormal();
                if (
                    triangles[i].normalVector.x * (triangles[i].p1.global.x - cameraPosition.x) +
                    triangles[i].normalVector.y * (triangles[i].p1.global.y - cameraPosition.y) + 
                    triangles[i].normalVector.z * (triangles[i].p1.global.z - cameraPosition.z) < 0
                    )
                {
                    projected.Add(triangles[i]);

                }
            }

            return projected;
        }

        public char getColor(float brightness)
        {
            int lum = (int)(13 * brightness);
            switch (lum)
            {
                // .:-=+*#%@
                case 0: return ' '; break;
                case 1: return '.'; break;
                case 2: return ':'; break;
                case 3: return '-'; break;
                case 4: return '='; break;
                case 5: return '+'; break;
                case 6: return '*'; break;
                case 7: return '#'; break;
                case 8: return '%'; break;
                case 9: return '@'; break;
                case 10: return '@'; break;

                default: return '█'; break;
            }
        }

        public bool LoadFromObjFile(String sfilename)
        {
            StreamReader reader = new StreamReader(sfilename);
            List<Point3D> verts = new List<Point3D>();
            List<Triangle3> trig = new List<Triangle3>();
            string line = reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {


                if (line[0] == 'v')
                {

                    string[] numbers = Regex.Split(line, @"(-?[0-9]+(?:[,.][0-9]+)?)");
                    
                    Console.WriteLine(numbers[0]); 
                    Console.WriteLine(numbers[1]); 

                    Console.WriteLine(numbers[3]);

                    Console.WriteLine(numbers[5]);
                    verts.Add(new Point3D(location,
                        Convert.ToDouble(numbers[1]) * scale,
                        Convert.ToDouble(numbers[3]) * scale,
                        Convert.ToDouble(numbers[5]) * scale
                        ));


                }
                if (line[0] == 'f')
                {

                    string[] numbers = Regex.Split(line, @"(-?[0-9]+(?:[,.][0-9]+)?)");
                    Console.WriteLine(numbers[0] + " "+ numbers[1] + " " + numbers[3] + " " + numbers[5]);

                    trig.Add(new Triangle3(
                        verts[Convert.ToInt32(numbers[1]) - 1],
                        verts[Convert.ToInt32(numbers[3]) - 1],
                        verts[Convert.ToInt32(numbers[5]) - 1]
                        ));


                }


            }


            triangles.AddRange(trig);

            verticies.AddRange(verts);

            return true;
        }
    }
}
