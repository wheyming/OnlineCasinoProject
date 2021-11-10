namespace Casino.WebAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsPrizeEnabled { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        void SetPrizeModuleStatus(int inputPrizeSetting);
    }
}
