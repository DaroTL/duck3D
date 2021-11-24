using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{
    public class Line3
    {
        public Point3D p1;
        public Point3D p2;
        
        public Line3(Point3D p1, Point3D p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
        public Line2 convertLine3(Vector3 cameraPosition, Vector3 cameraRotation, double cameraScreenDistance = 20)
        {
            Line2 newLine = new Line2(p1.projectSimple(cameraPosition, cameraRotation, cameraScreenDistance), p2.projectSimple(cameraPosition, cameraRotation, cameraScreenDistance));
            return newLine;
        }
    }
}
