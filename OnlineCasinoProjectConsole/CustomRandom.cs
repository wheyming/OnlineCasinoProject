using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    public class CustomRandom : ICustomRandom
    {
        Random randomizingNumber;

        public CustomRandom()
        {
            randomizingNumber = new Random();
        }
        public int randomInt1(int minimum, int maximum)
        {
            return randomizingNumber.Next(minimum, maximum);
        }
        public int randomInt2(int minimum, int maximum)
        {
            return randomizingNumber.Next(minimum, maximum);
        }
        public int randomInt3(int minimum, int maximum)
        {
            return randomizingNumber.Next(minimum, maximum);
        }
        public int randomIntMax(int maximum)
        {
            return randomizingNumber.Next(maximum);
        }
    }
}
