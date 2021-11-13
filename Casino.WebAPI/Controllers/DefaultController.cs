using Casino.WebAPI.EntityFramework;
using System.Web.Http;

namespace Casino.WebAPI.Controllers
{
    [RoutePrefix("api")]
    public class DefaultController : ApiController
    {
        [HttpGet]
        [Route("")]
        public void Initialize()
        {
            using (CasinoContext casinoContext = new CasinoContext())
            {
                casinoContext.Database.BeginTransaction();
            }
        }
    }
}
