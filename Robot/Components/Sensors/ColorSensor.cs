// Comments:
// [Special] __sBotics__Activate(), __sBotics__Deactivate()
//     ↪ Triggered in the beggining and end of games
// [Behavior] RenderedImage(Camera camera)
//     ↪ Renders an image and returns it

using UnityEngine;
using sBotics.CodeUtils;

namespace sBotics
{
    namespace Robot
    {
        public class ColorSensor : __sBotics__Sensor
        {   
            Camera sensorLens;

            private const int image_size = 5;

            public override bool Digital
            {
                get => (Analog.Closest() != Colors.Black);
            }

            public CodeUtils.Color Analog
            {
                get
                {                  
                    UnityEngine.Color[] colorArray = RenderedImage(sensorLens).GetPixels();

                    // Gets total image size, e.g. 5*5 = 25
                    int total_image_size = (image_size * image_size);
                    float _r = 0, _g = 0, _b = 0;

                    for (int i = 0; i < total_image_size; i++)
                    {
                        _r += colorArray[i].r;
                        _g += colorArray[i].g;
                        _b += colorArray[i].b;
                    }

                    // Returns average
                    return new CodeUtils.Color
                    (
                        (_r / total_image_size) * 256, 
                        (_g / total_image_size) * 256, 
                        (_b / total_image_size) * 256
                    );
                }
            }

            void Awake() => sensorLens.targetTexture = new RenderTexture(image_size, image_size, 8);

            Texture2D RenderedImage(Camera camera)
            {
                // The Render Texture in RenderTexture.active is the one that will be read by ReadPixels.
                var currentRT = RenderTexture.active;
                RenderTexture.active = camera.targetTexture;

                // Renders the camera's view.
                camera.Render();

                // Makes a new texture and read the active Render Texture into it.
                Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
                image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
                image.Apply();

                // Replaces the original active Render Texture.
                RenderTexture.active = currentRT;
                return image;
            }

            public override void __sBotics__Activate() {}
            public override void __sBotics__Deactivate() {}
        }
    }
}
