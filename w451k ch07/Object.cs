using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{
    class Object
    {
        public String name = "";
        public Vector3 location = new Vector3(0, 0, 0);
        public Vector3 origin = new Vector3(15, 15, 15);
        Vector3 rotation = new Vector3(0, 0, 0);
        public List<Point3D> verticies = new List<Point3D>();
        public List<Line3> lines = new List<Line3>();


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
                List<Line3> newLines = new List<Line3>();
                //znajduje po id
                foreach (Point3D y in verticies)
                {
                    
                    foreach (int c in x)
                    {
                        
                        if (y.getid() == c)
                        {
                            
                            toConnect.Add(y);
                        }
                    }    

                }
                //laczy 
                
                for (int z = 0; z < toConnect.Count - 1; z++)
                {
  
                        newLines.Add(new Line3(toConnect[z], toConnect[z+1]));
                        lines.Add(new Line3(toConnect[z], toConnect[z+1]));
                    if (z != 0 && (float)(z+1) % 3 == 0 && triangle) 
                    {
                        
                        newLines.Add(new Line3(toConnect[z], toConnect[z - 2]));
                        lines.Add(new Line3(toConnect[z], toConnect[z - 2]));
                    }
                    if(z + 1 == toConnect.Count - 1)
                    {
                        newLines.Add(new Line3(toConnect[0], toConnect[toConnect.Count - 1]));
                        lines.Add(new Line3(toConnect[0], toConnect[toConnect.Count - 1]));
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
    }
}
