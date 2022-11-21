// Comments:
// [Special] __sBotics__Activate(), __sBotics__Deactivate()
//     ↪ Triggered in the beggining and end of games
// [Behavior] RenderedImage(UnityEngine.Camera camera)
//     ↪ Renders an image and returns it

using UnityEngine;
using sBotics.CodeUtils;

namespace sBotics
{
    namespace Robot
    {
        public class Camera : __sBotics__RobotComponent
        {   
            UnityEngine.Camera sensorLens;

            private const int image_size = 128;

            public CodeUtils.Color[] Capture()
            {                 
                UnityEngine.Color[] colorArray = RenderedImage(sensorLens).GetPixels();
                CodeUtils.Color[] returnArray = new CodeUtils.Color[colorArray.Length];

                // Gets total image size, e.g. 128*128 = 16384
                int total_image_size = (image_size * image_size);

                // Converts Unity's color class to sBotics' color class
                for (int i = 0; i < total_image_size; i++)
                    returnArray[i] = new CodeUtils.Color(colorArray[i].r, colorArray[i].g, colorArray[i].b);

                // Returns converted array
                return returnArray;
            }

            void Awake() => sensorLens.targetTexture = new RenderTexture(image_size, image_size, 16);

            Texture2D RenderedImage(UnityEngine.Camera camera)
            {
                // The Render Texture in RenderTexture.active is the one that will be read by ReadPixels.
                var currentRT = RenderTexture.active;
                RenderTexture.active = camera.targetTexture;

                // Render the camera's view.
                camera.Render();

                // Make a new texture and read the active Render Texture into it.
                Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
                image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
                image.Apply();

                // Replace the original active Render Texture.
                RenderTexture.active = currentRT;
                return image;
            }

            // Behavior methods
            public override void __sBotics__Activate() {}
            public override void __sBotics__Deactivate() {}
        }
    }
}
