using FocusPoint.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FocusPoint.DAL.Configurations;

public class AppDbContext : DbContext
{
    public DbSet<MainNote> MainNotes { get; set; }
    public DbSet<SavedNote> SavedNotes { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<WorkSession> WorkSessions { get; set; }
    public DbSet<WorkStatistic> WorkStatistics { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


    }
}