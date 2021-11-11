using System.Collections.Generic;

namespace Casino.WebAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IRandomNumberGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<int> RollRandomNumberPrizeActivated();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<int> RollRandomNumberPrizeNotActivated();
    }
}
