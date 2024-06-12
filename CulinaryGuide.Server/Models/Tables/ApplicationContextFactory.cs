namespace CulinaryGuide.Server.Models.Tables;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ApplicationContextFactory: IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=culinarydb;User Id=sa;Password=QweAsdZxc!23;");
        return new ApplicationContext(optionsBuilder.Options);
    }
}
