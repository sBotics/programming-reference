// Comments:
// [External Class] sBotics.__sBotics__Programming
//     ↪ Manages user programs
// [External Class] sBotics.__sBotics__Translation
//     ↪ Manages translations from the sBotics language API 

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace sBotics
{
    namespace Robot
    {
        public class __sBotics__RobotController : MonoBehaviour
        {
            public Dictionary<string, __sBotics__RobotComponent> __sBotics__ComponentDictionary = new Dictionary<string, __sBotics__RobotComponent>();

            public void __sBotics__Error(string errorMessage)
            {
                CodeUtils.IO.PrintLine(__sBotics__Programming.Warning(errorMessage, true));
                CodeUtils.IO.OpenConsole();
                __sBotics__DeactivateComponents();
            }

            public void __sBotics__ActivateComponents()
            {
                foreach(__sBotics__RobotComponent component in __sBotics__ComponentDictionary.Values)
                    component.__sBotics__Activate();
            }

            public void __sBotics__DeactivateComponents()
            {
                foreach(__sBotics__RobotComponent component in __sBotics__ComponentDictionary.Values)
                    component.__sBotics__Deactivate();
            }

            public double Inclination
            {
                get => Math.Abs(transform.eulerAngles.x);
            }

            public double Compass
            {
                get => Math.Abs(transform.eulerAngles.y);
            }

            public double Speed
            {
                get
                {
                    Rigidbody rb = __sBotics__RigidBody;
                    return rb ? rb.velocity.magnitude : -1;
                }
            }
            
            public Rigidbody __sBotics__RigidBody
            {
                get
                {
                    Rigidbody rb = GetComponent<Rigidbody>();
                    if(rb) return rb;
                    
                    rb = transform.parent.GetComponent<Rigidbody>();
                    if(rb) return rb;

                    return null;
                }
            }

            public T GetComponent<T>(string name)
            {
                if(__sBotics__ComponentDictionary.ContainsKey(name))
                    return __sBotics__ComponentDictionary[name].GetComponent<T>();
                else 
                {
                    CodeUtils.IO.PrintLine(__sBotics__Programming.Warning($"{__sBotics__Translation.GetString("COMPONENT_NOT_FOUND")} {name}", true));
                    CodeUtils.IO.OpenConsole();
                    return default(T);
                }
            }

            public string[] Components() => __sBotics__ComponentDictionary.Keys.ToArray();
        }
    }
}
