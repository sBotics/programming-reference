// Comments:
// [Special] __sBotics__Activate(), __sBotics__Deactivate()
//     ↪ Triggered in the beggining and end of games

using UnityEngine;

namespace sBotics
{
    namespace Robot
    {
        public class Pen : __sBotics__RobotComponent
        {
            TrailRenderer tip;

            // Public properties
            public bool Working
            {
                get => tip.emitting;
            }

            public CodeUtils.Color Color
            {
                get => new CodeUtils.Color((double) tip.material.color.r, (double) tip.material.color.g, (double) tip.material.color.b);
            }

            // Public methods
            public void TurnOn(CodeUtils.Color color)
            {
                if(__sBotics__RobotController.__sBotics__Inactive) return;

                tip.material.color = new Color((float) color.Red, (float) color.Green, (float) color.Blue);
                tip.emitting = true;
            }

            public void TurnOff()
            {
                if(__sBotics__RobotController.__sBotics__Inactive) return;

                tip.emitting = false;
            }

            // Behavior methods
            public override void __sBotics__Activate() => TurnOff();
            public override void __sBotics__Deactivate() => TurnOff();
        }
    }
}