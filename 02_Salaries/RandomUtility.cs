using System;

namespace _02_Salaries
{
    public static class RandomUtility
    {
        private static Random rnd = new System.Random();
        public static int GetRandomInteger(int minimum, int maximum)
        {
            return rnd.Next(minimum, maximum);
        }
    }
}
