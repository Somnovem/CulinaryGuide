using Microsoft.EntityFrameworkCore;

namespace CulinaryGuide.Server.Models
{
    public class ApplicationContext : DbContext
    {
         public DbSet<User> Users { get; set; } = null!;
         public DbSet<Type> Types { get; set; } = null!;
         public DbSet<Cuisine> Cuisines { get; set; } = null!;
         public DbSet<Like> Likes { get; set; } = null!;
         public DbSet<Recipe> Recipes { get; set; } = null!;
         
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
