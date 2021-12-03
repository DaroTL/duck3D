using System;
using System.Collections.Generic;


namespace Duck.three_dimension_menagment
{
    class Polygon3
    {

        public List<Point3D> verts = new List<Point3D>();
        public List<Line3> lines = new List<Line3>();
        public Vector3 normalVector;

        public Polygon3(Point3D[] verts)
        {
            for(int x = 0; x < verts.Length; x++)
            {
                this.verts.Add(verts[x]);
                if (x == verts.Length - 1) lines.Add(new Line3(verts[x], verts[x + 1]));
                else lines.Add(new Line3(verts[x], verts[0]));

            }

            normalVector = new Vector3(0, 0, 0);

            for (int x = 0; x < verts.Length; x++)
            {
                Vector3 curr = verts[x].global;
                Vector3 next = verts[(x + 1) % verts.Length].global;

                normalVector = new Vector3(
                   normalVector.x += (curr.y - next.y) * (curr.z - next.z),
                   normalVector.x += (curr.z - next.z) * (curr.x - next.x),
                   normalVector.x += (curr.x - next.x) * (curr.y - next.y)
                );
            }

            Math3D.normalizeVector(normalVector);
        }
    }
}
