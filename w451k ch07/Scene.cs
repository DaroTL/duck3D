using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w451k_ch07
{
    public class Scene
    {
        public static Scene currentScene;

        public List<Object> ObjectList = new List<Object>();
        public List<Light> LightList = new List<Light>();

        public Scene()
        {

        }

        static public void setCurrentScene(Scene current)
        {
            currentScene = current;
        }

    }
}
