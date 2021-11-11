using Casino.WebAPI.Interfaces;
using Casino.WebAPI.Models;
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
        [HttpGet]
        [Route("setprizemodule")]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputPrizeSetting"></param>
        public string SetPrizeModuleStatus(int inputPrizeSetting)
        {
            if (inputPrizeSetting == 1)
            {
                //Set prizemodule in DB
                return "PrizeGivingModule has been activated.";
            }
            else if (inputPrizeSetting == 2)
            {
                //Set prizemodule in DB
                return "PrizeGivingModule has been activated.";
            }
            else
            {
                throw new FormatException();
            }
        }
    }
}
