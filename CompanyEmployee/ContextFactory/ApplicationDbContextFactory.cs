using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using Repository;

namespace CompanyEmployee.ContextFactory
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        //this is to return  a new instance of 
        //our ApplicationDbcontext class with provided options.

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                  b=>b.MigrationsAssembly("CompanyEmployee"));
            return new ApplicationDbContext(builder.Options);

        }
    }
}
