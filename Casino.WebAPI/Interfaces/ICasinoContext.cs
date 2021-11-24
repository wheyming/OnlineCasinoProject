using Casino.WebAPI.Models;
using System.Data.Entity;

namespace Casino.WebAPI.Interfaces
{
    public interface ICasinoContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Report> Reports { get; set; }
        DbSet<PrizeModule> PrizeModule { get; set; }
        int SaveChanges();
    }
}