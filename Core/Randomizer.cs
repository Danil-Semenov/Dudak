using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public static class Randomizer
    {
        private static Random random;

        public static IEnumerable<int> RandomNumbers(int maxValue)
        {
            while (true)
            {
                yield return random.Next(maxValue);
            }
        }

        public static int RandomNumber(int maxValue)
        {
            return random.Next(maxValue);
        }

        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static IEnumerable<object> RandomSort(object[] data)
        {
            for (int i = data.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                // обменять значения data[j] и data[i]
                var temp = data[j];
                data[j] = data[i];
                data[i] = temp;
            }
            return data;
        }
    }
}
