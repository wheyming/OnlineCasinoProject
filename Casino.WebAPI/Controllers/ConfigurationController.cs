using Casino.WebAPI.EntityFramework;
using Casino.WebAPI.Interfaces;
using Casino.WebAPI.Models;
using System.Linq;
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
                using (CasinoContext casinoContext = new CasinoContext())
                {
                    PrizeModule prizeModule = casinoContext.PrizeModule.Where(x => x.Identifier == 1).FirstOrDefault();
                    casinoContext.PrizeModule.Remove(prizeModule);
                    casinoContext.SaveChanges();
                    prizeModule = new PrizeModule(true);
                    casinoContext.PrizeModule.Add(prizeModule);
                    casinoContext.SaveChanges();
                }
                //Set prizemodule in DB
                return "PrizeGivingModule has been activated.";
            }
            else if (inputPrizeSetting == 2)
            {
                using (CasinoContext casinoContext = new CasinoContext())
                {
                    PrizeModule prizeModule = casinoContext.PrizeModule.Where(x => x.Identifier == 1).FirstOrDefault();
                    casinoContext.PrizeModule.Remove(prizeModule);
                    casinoContext.SaveChanges();
                    prizeModule = new PrizeModule(false);
                    casinoContext.PrizeModule.Add(prizeModule);
                    casinoContext.SaveChanges();
                }
                //Set prizemodule in DB
                return "PrizeGivingModule has been deactivated.";
            }
            else
            {
                return "Invalid input.";
            }
        }
    }
}
