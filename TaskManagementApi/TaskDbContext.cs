using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagementApi.Enums;
namespace TaskManagementApi;

public class TaskDbContext : DbContext
{
    public DbSet<TaskManagementApi.Entities.Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MyInMemoryDatabase");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskManagementApi.Entities.Task>()
            .Property(p => p.Status)
            .HasConversion<string>(new EnumToStringConverter<Status>());
        modelBuilder.Entity<Entities.Task>().HasData(
        new Entities.Task {Id = 1, Title = "Task 1", Description = "Task 1 Description", DueDate = DateTime.Today.AddDays(-1)},
        new Entities.Task {Id = 2, Title = "Task 2", Description = "Task 2 Description", DueDate = DateTime.Now}
        );
    }
}
