using Microsoft.EntityFrameworkCore;
using SweatSheet.Server;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<WorkoutActivity> Activities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite("Data Source=workouts.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Workout>().HasMany(w => w.Activities);

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

        modelBuilder
            .Entity<Exercise>()
            .HasData(
                new Exercise
                {
                    Id = 1,
                    PrimaryMuscleGroup = MuscleGroup.Chest,
                    ExerciseTitle = "Bench Press"
                },
                new Exercise
                {
                    Id = 2,
                    PrimaryMuscleGroup = MuscleGroup.Back,
                    ExerciseTitle = "Pull Ups"
                },
                new Exercise
                {
                    Id = 3,
                    PrimaryMuscleGroup = MuscleGroup.Shoulders,
                    ExerciseTitle = "Shoulder Press"
                }
            );

        modelBuilder
            .Entity<Workout>()
            .HasData(
                new Workout
                {
                    Id = 1,
                    Title = "Workout 1",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1),
                    Activities = []
                }
            );
    }
}
