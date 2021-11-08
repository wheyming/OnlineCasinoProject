using CasinoWebAPI.Interfaces;
using System;

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
        public void SetPrizeModuleStatus(int inputPrizeSetting)
        {
            if (inputPrizeSetting == 1)
            {
                IsPrizeEnabled = true;
            }
            else if (inputPrizeSetting == 2)
            {
                IsPrizeEnabled = false;
            }
            else
            {
                throw new FormatException();
            }
        }
    }
}
