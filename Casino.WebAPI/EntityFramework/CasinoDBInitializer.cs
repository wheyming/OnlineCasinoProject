using Casino.WebAPI.Models;
using System.Data.Entity;

namespace Casino.WebAPI.EntityFramework
{
    public class CasinoDBInitializer : CreateDatabaseIfNotExists<CasinoContext>
    {
        protected override void Seed(CasinoContext context)
        {
            User defaultUser = new User("Owner", "S1111110S", "99999990", "Owner", true);
            context.Users.Add(defaultUser);
            context.SaveChanges();

            PrizeModule defaultPrizeModule = new PrizeModule(false);
            context.PrizeModule.Add(defaultPrizeModule);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}