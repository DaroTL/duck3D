using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07.three_dimension_menagment
{
    public class Math3D
    {

        public static Vector3 normalizeVector(Vector3 vec)
        {

            return new Vector3(

                vec.x > 0? 
                1: 
                vec.x != 0? 
                -1: 0,
                
                vec.y > 0? 
                1: 
                vec.y != 0? 
                -1: 0,

                vec.z > 0 ?
                1 :
                vec.z != 0 ?
                -1 : 0

                );
        }

    }
}
