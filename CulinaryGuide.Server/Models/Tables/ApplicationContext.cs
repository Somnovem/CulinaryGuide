using Microsoft.EntityFrameworkCore;

namespace CulinaryGuide.Server.Models.Tables
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
        
        public override int SaveChanges()
        {
            HandleLikes();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleLikes();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void HandleLikes()
        {
            var addedEntries = ChangeTracker.Entries<Like>()
                .Where(e => e.State == EntityState.Added);

            var deletedEntries = ChangeTracker.Entries<Like>()
                .Where(e => e.State == EntityState.Deleted);

            foreach (var entry in addedEntries)
            {
                var recipeId = entry.Entity.RecipeId;
                var recipe = Recipes.FirstOrDefault(r => r.Id == recipeId);
                if (recipe != null)
                {
                    recipe.Likes++;
                }
            }

            foreach (var entry in deletedEntries)
            {
                var recipeId = entry.Entity.RecipeId;
                var recipe = Recipes.FirstOrDefault(r => r.Id == recipeId);
                if (recipe != null)
                {
                    recipe.Likes--;
                }
            }
        }
    }
}
