using System;
using CasinoWebAPI.Interfaces;

namespace CasinoWebAPI.Utility
{
    internal class RandomNumberGenerator : IRandomNumberGenerator
    {
        public int GetRandomNumber(int maximum)
        {
            return new Random().Next(maximum);
        }

        public int GetRandomNumber(int minimum, int maximum)
        {
            return new Random().Next(minimum, maximum);
        }
    }
}
