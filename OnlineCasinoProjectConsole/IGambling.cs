using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    public interface IGambling
    {
        string PlaySlot(double betAmount, string username);
    }
}
