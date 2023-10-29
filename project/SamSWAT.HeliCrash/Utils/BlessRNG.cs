using System;
using System.Collections.Generic;
using System.Linq;

namespace SamSWAT.HeliCrash.TyrianReboot
{
    internal static class BlessRNG
    {
        private static readonly Random Rng = new Random((int) DateTime.Now.Ticks);
        
        private static float Random(float a, float b)
        {
            var num = (float) Rng.NextDouble();
            return a + (b - a) * num;
        }

        public static T SelectRandom<T>(this IReadOnlyList<T> list)
        {
            if (list.Count == 0)
            {
                return default;
            }

            var index = Rng.Next(0, list.Count);
            return list[index];
        }
        
        public static bool RngBool(float chanceInPercent = 50f)
        {
            return Random(0f, 100f) < chanceInPercent;
        }
        
        internal static List<T> Shuffle<T>(this List<T> l)
        {
            return l.OrderBy(x => Random(0.0f, 5f)).ToList();
        }
    }
}