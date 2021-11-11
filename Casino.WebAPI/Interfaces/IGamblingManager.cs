using Casino.Common;
using System;
using System.Collections.Generic;

namespace Casino.WebAPI.Interfaces
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
        (IList<int>, double, SlotsResultType, double, DateTime) PlaySlot(double betAmount, string username);
    }
}
