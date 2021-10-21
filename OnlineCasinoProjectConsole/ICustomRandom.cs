using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    public interface ICustomRandom
    {
        int randomInt1(int minimum, int maximum);

        int randomInt2(int minimum, int maximum);
        int randomInt3(int minimum, int maximum);

        int randomIntMax(int maximum);
    }
}
