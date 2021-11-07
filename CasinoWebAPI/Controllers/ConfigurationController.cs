using CasinoWebAPI.Interfaces;

namespace CasinoWebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    internal class ConfigurationController : IConfigurationManager
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsPrizeEnabled { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public void SetPrizeModuleStatus(bool status)
        {
            IsPrizeEnabled = status;
        }
    }
}
