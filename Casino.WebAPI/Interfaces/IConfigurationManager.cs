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
        /// <param name="status"></param>
        string SetPrizeModuleStatus(int inputPrizeSetting);
    }
}
