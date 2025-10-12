using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;

namespace WebApp_dotNet.Persistence;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
    {
    }

    public DbSet<BidDb> BidDbs { get; set; }
    public DbSet<AuctionDb> AuctionDbs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AuctionDb pdb = new AuctionDb
        {
            Id = -1, //seed data
            Title = "Learn ASP.NET Core with MVC",
            Description = "If you want to learn more, you know where to look! Ehe!",
            CreatedDate = DateTime.Now,
            StartingPrice = 300,
            UserName = "Bob@kth.se",
            EndDate = new DateTime(2077, 07, 20),
            BidDbs = new List<BidDb>()
        };
        modelBuilder.Entity<AuctionDb>().HasData(pdb);

        BidDb tdb1 = new BidDb()
        {
            Id = -1, //seed data
            DateAdded = DateTime.Now,
            AuctionId = -1,
            Username = "Bob@kth.se"
        };
        modelBuilder.Entity<BidDb>().HasData(tdb1);
    }
}