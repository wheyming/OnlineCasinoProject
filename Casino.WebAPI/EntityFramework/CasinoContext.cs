using Casino.WebAPI.Models;
using System.Data.Entity;

namespace Casino.WebAPI.EntityFramework
{
    public class CasinoContext : DbContext
    {
        public CasinoContext() : base(
#if DEBUG
            "Name = DebugCasinoDBConnectionString"
#else
            "Name = ReleaseCasinoDBConnectionString"
#endif
            )
        {
            Database.SetInitializer(new CasinoDBInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<PrizeModule> PrizeModule { get; set; }
    }
}