using Casino.WebAPI.EntityFramework;
using Casino.WebAPI.Interfaces;
using Casino.WebAPI.Models;
using System.Linq;
using System.Web.Http;

namespace Casino.WebAPI.Controllers
{
    /// <summary>
    /// Configuration Controller to set up the configurations of the casino.
    /// </summary>
    [RoutePrefix("api/Configuration")]
    public class ConfigurationController : ApiController, IConfigurationManager
    {
        private readonly ICasinoContext _casinoContext;
        private readonly string _connectionString;
        public ConfigurationController()
        {
#if DEBUG
            _connectionString = "DebugCasinoDBConnectionString";
#else
            _connectionString = "ReleaseCasinoDBConnectionString";
#endif
            _casinoContext = new CasinoContext(_connectionString);
        }

        public ConfigurationController(ICasinoContext casinoContext)
        {
            _casinoContext = casinoContext;
        }
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
                PrizeModule prizeModule = _casinoContext.PrizeModule.Where(x => x.Identifier == 1).FirstOrDefault();
                _casinoContext.PrizeModule.Remove(prizeModule);
                _casinoContext.SaveChanges();
                prizeModule = new PrizeModule(true);
                _casinoContext.PrizeModule.Add(prizeModule);
                _casinoContext.SaveChanges();
                //Set prizemodule in DB
                return "PrizeGivingModule has been activated.";
            }
            else if (inputPrizeSetting == 2)
            {
                PrizeModule prizeModule = _casinoContext.PrizeModule.Where(x => x.Identifier == 1).FirstOrDefault();
                _casinoContext.PrizeModule.Remove(prizeModule);
                _casinoContext.SaveChanges();
                prizeModule = new PrizeModule(false);
                _casinoContext.PrizeModule.Add(prizeModule);
                _casinoContext.SaveChanges();
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
