
using System;
using w451k_ch07.three_dimension_menagment;

namespace w451k_ch07
{
    public class Triangle3
    {
        public Point3D p1;
        public Point3D p2;
        public Point3D p3; 
        public Line3[] lines = new Line3[3];
        public Vector3 normalVector;
        public char sym;
        public float brightness = 0;

        public Triangle3(Point3D p1, Point3D p2, Point3D p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            lines[0] = new Line3(p1, p2);
            lines[1] = new Line3(p2, p3);
            lines[2] = new Line3(p3, p1);   
            normalVector = new Vector3(0, 0, 0);
            recalculateNormal();
        }


        public void flipNormal()
        {
            normalVector.z = normalVector.z == 0 ? 0 : normalVector.z > 0 ? -1 : 1;
        }

        public void recalculateNormal()
        {
            /*            Vector3 U = new Vector3(
                            p2.global.x - p1.global.x,
                            p2.global.y - p1.global.y,
                            p2.global.z - p1.global.z
                            );
                        Vector3 V = new Vector3(
                            p3.global.x - p1.global.x,
                            p3.global.y - p1.global.y,
                            p3.global.z - p1.global.z
                            );
                        normalVector = new Vector3(
                               (U.y * V.z) - (U.z * V.y),
                               (U.z * V.x) - (U.x * V.z),
                               (U.x * V.y) - (U.y * V.x)
                            );
                        normalVector = Math3D.normalizeVector(normalVector);*/

            Vector3 line1 = new Vector3(
                p2.global.x - p1.global.x,
                p2.global.y - p1.global.y,
                p2.global.z - p1.global.z
                );

            Vector3 line2 = new Vector3(
                p3.global.x - p1.global.x,
                p3.global.y - p1.global.y,
                p3.global.z - p1.global.z
                );

            normalVector.x = (line1.y * line2.z) - (line1.z * line2.y);
            normalVector.y = (line1.z * line2.x) - (line1.x * line2.z);
            normalVector.z = (line1.x * line2.y) - (line1.y * line2.x);

            double l = Math.Sqrt(normalVector.x * normalVector.x + normalVector.y * normalVector.y + normalVector.z * normalVector.z);
            normalVector.x /= l;
            normalVector.y /= l;
            normalVector.z /= l;

        }
    }
}
