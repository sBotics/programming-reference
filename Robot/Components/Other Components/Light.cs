// Comments:
// [Behavior] Awake()
//     ↪ Triggered as soon as the object is created
// [Special] __sBotics__Activate(), __sBotics__Deactivate()
//     ↪ Triggered in the beggining and end of games

using UnityEngine;

namespace sBotics
{
    namespace Robot
    {
        public class Light : __sBotics__RobotComponent
        {
            MeshRenderer diode;

            const float offAlpha = 0.4f;
            const float onAlpha = 0.8f;

            void Awake() => TurnOff();

            public bool Lit
            {
                get => (diode.material.color.a == onAlpha);
            }

            public CodeUtils.Color Color
            {
                get => new CodeUtils.Color(diode.material.color.r*256, diode.material.color.g*256, diode.material.color.b*256);
            }

            public void TurnOn(CodeUtils.Color color) =>
                GetComponent<MeshRenderer>().material.color = diode.material.color = new UnityEngine.Color((float) color.Red/256, (float) color.Green/256, (float) color.Blue/256, onAlpha);

            public void TurnOff() =>
                GetComponent<MeshRenderer>().material.color = diode.material.color = new UnityEngine.Color(65/256f, 70/256f, 144/256f, offAlpha);

            public override void __sBotics__Activate() => TurnOff();
            public override void __sBotics__Deactivate() => TurnOff();
        }
    }
}