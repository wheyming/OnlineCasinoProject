using Casino.WebAPI.Interfaces;
using Casino.WebAPI.Models;
using System.Data.Entity;

namespace Casino.WebAPI.EntityFramework
{
    public class CasinoContext : DbContext, ICasinoContext
    {
        public CasinoContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new CasinoDBInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<PrizeModule> PrizeModule { get; set; }
    }
}