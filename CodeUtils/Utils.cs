// Comments:
// [Alias] SetPrecision
//     ↪ Math.Round alias to use with doubles (rEduc)

using UnityEngine;
using System;

namespace sBotics
{
    namespace CodeUtils
    {
        public static class Utils
        {
            public static double Clamp(double value, double minValue, double MaxValue)
            {
                double clamped = ((value < minValue)? minValue : value);
                return ((clamped > MaxValue)? MaxValue : clamped);
            }

            public static double Map(double value, double minA, double maxA, double minB, double maxB) =>
                (value - minA) * (maxB - minB) / (maxA - minA) + minB;

            public static double Random(double min, double max) =>
                (double) Random.Range((float) min, (float) max);

            public static double SetPrecision(double value, double precision) => 
                Math.Round(value, (int) precision);

            public static double Modulo(double value, double mod) => (value % mod + mod) % mod;
        }
    }
}

