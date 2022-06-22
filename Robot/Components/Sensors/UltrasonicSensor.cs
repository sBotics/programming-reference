// Comments:
// [Special] __sBotics__Activate(), __sBotics__Deactivate()
//     ↪ Triggered in the beggining and end of games

using UnityEngine;
using System;

namespace sBotics
{
    namespace Robot
    {
        public class UltrasonicSensor : __sBotics__Sensor
        {
            Transform leftRayPosition; 
            Transform rightRayPosition;

            // LayerMask, ignore layer(s):
            // Ignore Raycast (layer 2), Game Debugging (layer 16), Forbidden (layer 17)
            const int mask = (1 << 2) | (1 << 16) | (1 << 17);

            public override bool Digital
            {
                get => (Analog != -1);
            }

            public double Analog
            {
                get
                {
                    // Generates three rays to find the closest value
                    double _closest = Math.Min(Raycast(leftRayPosition), Math.Min(Raycast(transform), Raycast(rightRayPosition)));

                    // Returns the closest or -1 if the value is too high 
                    return (_closest == double.MaxValue ? -1: _closest);
                }
            }

            // Creates raycast and checks for the distance
            double Raycast(Transform _transform)
            {
                Ray ray = new Ray(_transform.position, _transform.forward);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit, 500f, ~mask))
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.green);
                    return (hit.distance > 32 ? double.MaxValue : hit.distance);
                }
                else
                    return double.MaxValue;
            }

            public override void __sBotics__Activate() {}
            public override void __sBotics__Deactivate() {}
        }
    }
}
