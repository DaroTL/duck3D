using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duck
{

    public class Point3D
    {
        public static int idCount = 0;
        private int id = 0;

        public Vector3 local;
        public Vector3 global;

        public Point3D(Vector3 global, double x, double y, double z, int _id = 0)
        {
            this.local = new Vector3(x, y, z);
            this.global = new Vector3(x + global.x, y + global.y, z + global.z);
            if (_id == 0) id = idCount;
            else id = _id;
            idCount++;
        }

        public int getid()
        {
            return id;
        }

        public Vector2 projectSimple()
        {
            if(Camera.currentCamera != null)
            {
                Vector3 camPos = Camera.currentCamera.cameraPosition;
                Vector3 camRot = Camera.currentCamera.cameraRotation;

                Vector3 a = global;
                Vector3 e = new Vector3(camPos.x, camPos.y, camPos.z + Camera.currentCamera.cameraScreenDistance);
                Matrix d = new Matrix(new double[3, 1]);



                Matrix mx = new Matrix(new double[,] {
                { 1, 0, 0 },
                { 0, Math.Cos(camRot.x), Math.Sin(camRot.x) },
                { 0, -Math.Sin(camRot.x), Math.Cos(camRot.x) }
            });


                Matrix my = new Matrix(new double[,] {
                { Math.Cos(camRot.y), 0, -Math.Sin(camRot.y) },
                { 0, 1, 0 },
                { Math.Sin(camRot.y), 0, Math.Cos(camRot.y) }
            });


                Matrix mz = new Matrix(new double[,] {
                { Math.Cos(camRot.z), Math.Sin(camRot.z), 0 },
                { -Math.Sin(camRot.z), Math.Cos(camRot.z), 0 },
                { 0, 0, 1 }
            });


                Matrix aminusc =
                    new Matrix(new double[,] {
                    { a.x }, { a.y }, { a.z } }
                    )
                    .subtract(
                        new Matrix(new double[,] {
                        {camPos.x }, { camPos.y }, { camPos.z }
                        }));


                d = mx.multiply(my).multiply(mz).multiply(aminusc);
                Vector3 dVector = new Vector3(d.tablica[0, 0], d.tablica[1, 0], d.tablica[2, 0]);
                Vector2 vect = new Vector2((int)((e.z / dVector.z * dVector.x) + e.x), (int)((e.z / dVector.z * dVector.y) + e.y));
                return vect;
            }


            return new Vector2(0,0);

        }



        public void setGlobal(Vector3 x)
        {
            global.x += x.x;
            global.y += x.y;
            global.z += x.z;
        }
    }
}
