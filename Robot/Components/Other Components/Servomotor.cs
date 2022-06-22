// Comments:
// [External Class] sBotics.__sBotics__CorrectHingeAngle
//     ↪ Returns the current motor (Unityengine.HingeJoint) angles and velocity
// [External Class] sBotics.__sBotics__WaitFor
//     ↪ Used in Coroutines to wait for a certain amount of time / no. of frames
// [Behavior] Awake()
//     ↪ Triggered as soon as the object is created
// [Special] __sBotics__Activate(), __sBotics__Deactivate()
//     ↪ Triggered in the beggining and end of games

using UnityEngine;
using System;

namespace sBotics
{
    namespace Robot
    {
        public class Servomotor : __sBotics__RobotComponent
        {
            const int maxTargetVelocity = 500;
            double force = 0, target = 0;
            bool locked = true;

            HingeJoint joint;
            void Awake()
            {
                joint = GetComponent<HingeJoint>();

                // Waits two frames and locks the motor
                StartCoroutine(sBotics.Utils.__sBotics__WaitFor.Frames(2, () =>
                    gameObject.AddComponent<FixedJoint>().connectedBody = joint.connectedBody
                ));
            }

            public bool Locked
            {
                get => locked;
                set
                {
                    if(locked == value) return;
                    locked = value;

                    if(!joint) return;
                    
                    // Disables motor
                    joint.useMotor = !locked;

                    // Locks motor rotation
                    if(locked) gameObject.AddComponent<FixedJoint>().connectedBody = joint.connectedBody;
                    else Destroy(GetComponent<FixedJoint>());
                }
            }

            public double Angle
            {
                get => GetComponent<__sBotics__CorrectHingeAngle>().Angle();
            }

            public double Velocity
            {
                get => GetComponent<__sBotics__CorrectHingeAngle>().Velocity();
            }
            
            public double Force
            {
                get => force;
                set
                {
                    force = Math.Abs(value);

                    if(!joint) return;

                    JointMotor motor = joint.motor;
                    motor.force = (float) force;
                    joint.motor = motor;
                }
            }

            public double Target
            {
                get => target;
                set
                {
                    target = CodeUtils.Utils.Clamp(value, -maxTargetVelocity, maxTargetVelocity);

                    if(!joint) return;

                    JointMotor motor = joint.motor;
                    motor.targetVelocity = (float) target;
                    joint.motor = motor;
                }
            }

            public void Apply(double _force, double _target)
            {
                Force = _force;
                Target = _target;
            }

            public override void __sBotics__Activate()
            {
                Force = 0;
            }

            public override void __sBotics__Deactivate()
            {
                Force = 0;
                Locked = true;
            }
        }
    }
}