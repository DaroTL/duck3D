namespace w451k_ch07
{
    public class Camera
    {
        public Vector3 cameraPosition;
        public Vector3 cameraRotation;
        public double cameraScreenDistance = 20;
        public string name;

        static public Camera currentCamera;

        public Camera(Vector3 cameraPosition, Vector3 cameraRotation, double cameraScreenDistance, string name)
        {
            this.name = name;
            this.cameraPosition = cameraPosition;
            this.cameraRotation = cameraRotation;
            this.cameraScreenDistance = cameraScreenDistance;
        }

        public static void setCurrentCamera(Camera cam)
        {
            currentCamera = cam;
        }
    }
}