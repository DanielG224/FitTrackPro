using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FitTrackPro.Models;

namespace FitTrackPro.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkoutPlanExercise>()
                .HasKey(x => new { x.WorkoutPlanId, x.ExerciseId });

            modelBuilder.Entity<WorkoutPlanExercise>()
                .HasOne(x => x.WorkoutPlan)
                .WithMany(x => x.WorkoutPlanExercises)
                .HasForeignKey(x => x.WorkoutPlanId);

            modelBuilder.Entity<WorkoutPlanExercise>()
                .HasOne(x => x.Exercise)
                .WithMany(x => x.WorkoutPlanExercises)
                .HasForeignKey(x => x.ExerciseId);
        }
        public DbSet<CmsContent> CmsContents { get; set; }
    }
}