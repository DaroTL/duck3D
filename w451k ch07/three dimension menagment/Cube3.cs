using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duck
{
    public class Cube3
    {

        public Point3D[] points;
        public Line3[] lines;
        public Pane3[] panes;

        public Cube3(Point3D _p1, Point3D _p2, Point3D _p3, Point3D _p4, Point3D _p5, Point3D _p6, Point3D _p7, Point3D _p8)
        {
            points = new Point3D[] { _p1, _p2, _p3, _p4, _p5, _p6, _p7, _p8 };
            lines = new Line3[] 
            {
                new Line3(points[0], points[1]),
                new Line3(points[0], points[7]),
                new Line3(points[0], points[3]),
                new Line3(points[2], points[1]),
                new Line3(points[2], points[3]),
                new Line3(points[2], points[5]),
                new Line3(points[3], points[4]),
                new Line3(points[1], points[6]),    
                new Line3(points[7], points[4]),
                new Line3(points[4], points[5]),
                new Line3(points[5], points[6]),
                new Line3(points[6], points[7])
            };
            panes = new Pane3[]
            {
                new Pane3(points[0], points[1], points[2], points[3]),
                new Pane3(points[7], points[6], points[5], points[4]),
                new Pane3(points[2], points[3], points[4], points[5]),
                new Pane3(points[1], points[6], points[7], points[0]),
                new Pane3(points[0], points[3], points[4], points[7]),
                new Pane3(points[1], points[2], points[5], points[6]),


            };

        }

    }
}
