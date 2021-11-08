using CasinoWebAPI.Common;
using System.Collections.Generic;

namespace CasinoWebAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IGamblingManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="betAmount"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        (IList<int>, double, SlotsResultType) PlaySlot(double betAmount, string username);
    }
}
