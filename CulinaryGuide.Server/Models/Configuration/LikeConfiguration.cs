using Microsoft.EntityFrameworkCore;

namespace CulinaryGuide.Server.Models.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
    where T : class
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey("Id");
        builder.ToTable(typeof(T).Name);
        ConfigureInternal(builder);
    }
    protected abstract void ConfigureInternal(EntityTypeBuilder<T> builder);
}

public class LikeConfiguration : BaseEntityTypeConfiguration<Like>
{
    protected override void ConfigureInternal(EntityTypeBuilder<Like> builder)
    {
        builder.HasOne(l => l.User)
            .WithMany()
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(l => l.Recipe)
            .WithMany()
            .HasForeignKey(l => l.RecipeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}