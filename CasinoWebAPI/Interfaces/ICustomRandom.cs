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
        /// <param name="maximum"></param>
        /// <returns></returns>
        int GetRandomNumber(int maximum);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        int GetRandomNumber(int minimum, int maximum);
    }
}
