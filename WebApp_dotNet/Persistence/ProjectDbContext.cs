using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;

namespace WebApp_dotNet.Persistence;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
    {
    }

    public DbSet<TaskDb> TaskDbs { get; set; }
    public DbSet<ProjectDb> ProjectDbs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ProjectDb pdb = new ProjectDb
        {
            Id = -1, //seed data
            Title = "Learn ASP.NET Core with MVC",
            CreatedDate = DateTime.Now,
            UserName = "Bob@kth.se",
            TaskDBs = new List<TaskDb>()
        };
        modelBuilder.Entity<ProjectDb>().HasData(pdb);

        TaskDb tdb1 = new TaskDb()
        {
            Id = -1, //seed data
            Description = "Follow the tutorials",
            LastUpdated = DateTime.Now,
            status = Core.Status.IN_PROGRESS,
            ProjectId = -1
        };
        modelBuilder.Entity<TaskDb>().HasData(tdb1);

        TaskDb tdb2 = new TaskDb()
        {
            Id = -2, //seed data
            Description = "Do it yourself!",
            LastUpdated = DateTime.Now,
            status = Core.Status.DONE,
            ProjectId = -1
        };
        modelBuilder.Entity<TaskDb>().HasData(tdb2);
    }
}