namespace sBotics
{
    namespace Robot
    {
        public abstract class __sBotics__RobotComponent : MonoBehaviour
        {
            public string Name;

            public abstract void __sBotics__Activate();
            public abstract void __sBotics__Deactivate();
        }
    }
}