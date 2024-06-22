using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IMS.Data.Repository
{
    public class Net1710_221_4_IMSContextFactory : IDesignTimeDbContextFactory<Net17102214ImsContext>
    {
        public Net17102214ImsContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<Net17102214ImsContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            
            return new Net17102214ImsContext(optionsBuilder.Options);
        }
    }
}