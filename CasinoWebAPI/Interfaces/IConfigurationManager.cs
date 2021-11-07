namespace CasinoWebAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IConfigurationManager
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsPrizeEnabled { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        void SetPrizeModuleStatus(bool status);
    }
}
