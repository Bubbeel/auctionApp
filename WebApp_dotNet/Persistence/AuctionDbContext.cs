using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;

namespace WebApp_dotNet.Persistence;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
    {
    }

    public DbSet<bidDb> TaskDbs { get; set; }
    public DbSet<AuctionDb> AuctionDbs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AuctionDb pdb = new AuctionDb
        {
            Id = -1, //seed data
            Title = "Learn ASP.NET Core with MVC",
            CreatedDate = DateTime.Now,
            UserName = "Bob@kth.se",
            BidDbs = new List<bidDb>()
        };
        modelBuilder.Entity<AuctionDb>().HasData(pdb);

        bidDb tdb1 = new bidDb()
        {
            Id = -1, //seed data
            Description = "Follow the tutorials",
            LastUpdated = DateTime.Now,
            status = Core.Status.IN_PROGRESS,
            ProjectId = -1
        };
        modelBuilder.Entity<bidDb>().HasData(tdb1);

        bidDb tdb2 = new bidDb()
        {
            Id = -2, //seed data
            Description = "Do it yourself!",
            LastUpdated = DateTime.Now,
            status = Core.Status.DONE,
            ProjectId = -1
        };
        modelBuilder.Entity<bidDb>().HasData(tdb2);
    }
}