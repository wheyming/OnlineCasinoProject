namespace OnlineCasinoProjectConsole
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICasinoConfiguration
    {
        bool IsPrizeEnabled { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        void SetPrizeModuleStatus(bool status);
    }
}
