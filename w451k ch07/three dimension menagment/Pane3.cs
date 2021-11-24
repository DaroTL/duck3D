using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{
    public class Pane3
    {
        public Point3D p1;
        public Point3D p2;
        public Point3D p3;
        public Point3D p4;
        public List<Line3> lines = new List<Line3>();

        public Pane3(Point3D p1, Point3D p2, Point3D p3, Point3D p4)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.p4 = p4;
            lines.Add(new Line3(p1, p2));
            lines.Add(new Line3(p2, p3));
            lines.Add(new Line3(p3, p4));
            lines.Add(new Line3(p4, p1));
        }

        public Pane2 convertPane3(Vector3 cameraPosition, Vector3 cameraRotation, double cameraScreenDistance = 20)
        {
            return new Pane2(p1.projectSimple(
                cameraPosition, cameraRotation, cameraScreenDistance),
                p2.projectSimple(cameraPosition, cameraRotation, cameraScreenDistance),
                p3.projectSimple(cameraPosition, cameraRotation, cameraScreenDistance),
                p4.projectSimple(cameraPosition, cameraRotation, cameraScreenDistance)
                ); 
        }
    }
}
