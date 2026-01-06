using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFCoreNews.Data;

public class NewsDbContextFactory
    : IDesignTimeDbContextFactory<NewsDbContext>
{
    public NewsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NewsDbContext>();
//saifaldin
        optionsBuilder.UseSqlServer(
            "Server=Saifaldin\\SQLEXPRESS02;Database=EFCoreNews;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
            x => x.UseHierarchyId()
         );

        return new NewsDbContext(optionsBuilder.Options);
    }
}
