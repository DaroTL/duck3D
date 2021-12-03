using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duck
{
    public class Light
    {
        public Point3D location;
        public Vector3 lightDir;
        public string name;
        public Light(Point3D location, string name)
        {
            this.location = location;
            this.name = name;
        }
    }
}
