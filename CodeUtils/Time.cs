using System.Threading.Tasks;
using System;

namespace sBotics
{
    namespace CodeUtils
    {
        public static class Time
        {
            public static double Timestamp
            {
                get => ((DateTimeOffset) DateTime.Now).ToUnixTimeSeconds();
            }

            public static async Task Delay(double miliseconds) =>
                await Task.Delay((int) Utils.Clamp(miliseconds, 20, int.MaxValue), __sBotics__TaskToken.Token.Token);
        }
    }
}
