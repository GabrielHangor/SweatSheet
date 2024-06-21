using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SweatSheet.Server.Modules.Auth.Entities;
using SweatSheet.Server.Modules.Exercise.Entities;
using SweatSheet.Server.Modules.Workouts.Entities;

namespace SweatSheet.Server;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Workout> Workouts { get; init; }
    public DbSet<Exercise> Exercises { get; init; }
    public DbSet<WorkoutActivity> Activities { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<ApplicationRole>()
            .HasData(
                new ApplicationRole { Name = "user", NormalizedName = "USER" },
                new ApplicationRole { Name = "admin", NormalizedName = "ADMIN" }
            );

        modelBuilder.Entity<ApplicationUser>(b =>
        {
            b.HasMany(u => u.Workouts);
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.HasIndex(w => w.Id);
            entity.HasMany(w => w.Activities);
        });

        modelBuilder
            .Entity<WorkoutActivity>()
            .OwnsMany(
                a => a.Sets,
                s =>
                {
                    s.WithOwner().HasForeignKey("ActivityId");
                    s.Property("Id");
                    s.HasKey("Id");
                }
            );
    }
}
