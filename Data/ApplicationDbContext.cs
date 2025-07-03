using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BaseValue> BaseValues { get; set; } = null!;
        public DbSet<Food> Foods { get; set; } = null!;
        public DbSet<MealFollow> MealFollows { get; set; } = null!;
        public DbSet<MifflinStJeor> MifflinStJeors { get; set; } = null!;
        public DbSet<ProfilActivity> ProfilActivities { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Relation MealFollow -> Food
            modelBuilder.Entity<MealFollow>()
                .HasOne(mf => mf.Food)
                .WithMany()
                .HasForeignKey(mf => mf.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
