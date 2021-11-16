using Casino.WebAPI.EntityFramework;
using Casino.WebAPI.Interfaces;
using System.Web.Http;

namespace Casino.WebAPI.Controllers
{
    [RoutePrefix("api")]
    public class InitializeController : ApiController, IInitializeManager
    {
        private string _connectionString;
        public InitializeController()
        {
#if DEBUG
            _connectionString = "DebugCasinoDBConnectionString";
#else
            _connectionString = "ReleaseCasinoDBConnectionString";
#endif
        }

        [HttpGet]
        [Route("")]
        public void Initialize()
        {
            using (CasinoContext casinoContext = new CasinoContext(_connectionString))
            {
                casinoContext.Database.BeginTransaction();
            }
        }
    }
}
