// Comments:
// [Special] __sBotics__Activate(), __sBotics__Deactivate()
//     ↪ Triggered in the beggining and end of games
// [Behavior] OnTriggerEnter(Collider col)
//     ↪ Triggered when an object (col) collides with the sensor
// [Behavior] OnTriggerExit(Collider col)
//     ↪ Triggered when a collided object (col) stops colliding with the sensor

using UnityEngine;

namespace sBotics
{
    namespace Robot
    {
        public class TouchSensor : __sBotics__Sensor
        {
            Transform Cube;
            bool _triggered = false;

            public override bool Digital
            {
                get => _triggered;
            }

            private void OnTriggerEnter(Collider col) 
            {
                // Ingores collisions with certain objects
                if(col.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast")
                || col.gameObject.layer == LayerMask.NameToLayer("Game Debugging"))
                    return;

                _triggered = true;
                Cube.localPosition = new Vector3(0, 0, 0.25f);
            }

            private void OnTriggerExit(Collider col) 
            {
                // Ingores collisions with certain objects
                if(col.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast")
                || col.gameObject.layer == LayerMask.NameToLayer("Game Debugging"))
                    return;

                _triggered = false;
                Cube.localPosition = new Vector3(0, 0, 0.4f);
            }

            public override void __sBotics__Activate() {}
            public override void __sBotics__Deactivate() {}
        }
    }
}
