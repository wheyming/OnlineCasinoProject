using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    class CustomRandom : ICustomRandom
    {
        public int randomInt1(int minimum, int maximum)
        {
            Random randomizingNumber = new Random();
            return randomizingNumber.Next(minimum, maximum);
        }
        public int randomInt2(int minimum, int maximum)
        {
            Random randomizingNumber = new Random();
            return randomizingNumber.Next(minimum, maximum);
        }
        public int randomInt3(int minimum, int maximum)
        {
            Random randomizingNumber = new Random();
            return randomizingNumber.Next(minimum, maximum);
        }
        public int randomIntMax(int maximum)
        {
            Random randomizingNumber = new Random();
            return randomizingNumber.Next(maximum);
        }
    }
}
