using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Vector3
    {
        private double x;
        private double y;
        private double z;

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double getX()
        {
            return x;
        }
        
        public void setX(double value)
        {
            x = value;
        }

        public double getY()
        {
            return y;
        }

        public void setY(double value)
        {
            y = value;
        }

        public double getZ()
        {
            return z;
        }

        public void setZ(double value)
        {
            z = value;
        }
    }
}
