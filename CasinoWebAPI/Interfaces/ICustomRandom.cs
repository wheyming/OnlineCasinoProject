using System.Collections.Generic;

namespace CasinoWebAPI.Interfaces
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
