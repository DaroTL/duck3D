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

        public Pane2 convertPane3()
        {
            return new Pane2(p1.convertVectorTo2D(), p2.convertVectorTo2D(), p3.convertVectorTo2D(), p4.convertVectorTo2D()); 
        }
    }
}
