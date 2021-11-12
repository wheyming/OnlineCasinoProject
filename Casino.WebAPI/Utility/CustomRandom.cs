using Casino.WebAPI.Interfaces;
using System;
using System.Collections.Generic;

namespace Casino.WebAPI.Utility
{
    internal class RandomNumberGenerator : IRandomNumberGenerator
    {
        Random randomGenerator;
        public RandomNumberGenerator()
        {
            randomGenerator = new Random();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<int> RollRandomNumberPrizeActivated()
        {
            IList<int> numbers = new List<int>();
            numbers.Add(randomGenerator.Next(9));
            numbers.Add(randomGenerator.Next(9));
            numbers.Add(randomGenerator.Next(9));
            return numbers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<int> RollRandomNumberPrizeNotActivated()
        {
            int[] slotNumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 8, 9 };
            IList<int> numbers = new List<int>();
            numbers.Add(randomGenerator.Next(9));
            numbers.Add(randomGenerator.Next(9));
            numbers.Add(slotNumbers[randomGenerator.Next(slotNumbers.Length)]);
            return numbers;
        }
    }
}
