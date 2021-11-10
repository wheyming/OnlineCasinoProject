using Casino.WebAPI.Interfaces;
using System;
using System.Web.Http;

namespace Casino.WebAPI.Controllers
{
    [RoutePrefix("api/Configuration")]
    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationController : ApiController, IConfigurationManager
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsPrizeEnabled { get; private set; }
        [HttpGet]
        [Route("setprizemodule")]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputPrizeSetting"></param>
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
