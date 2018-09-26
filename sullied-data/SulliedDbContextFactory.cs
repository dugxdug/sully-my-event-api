using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace sullied_data
{
    public class SulliedDbCOntextFactory : IDesignTimeDbContextFactory<SulliedDbContext>
    {
        public SulliedDbContext CreateDbContext(string[] args)
        {
            var migrationsAssembly = typeof(SulliedDbCOntextFactory).GetTypeInfo().Assembly.GetName().Name;

            var optionsBuilder = new DbContextOptionsBuilder<SulliedDbContext>();
            optionsBuilder.UseSqlServer("Persist Security Info=False;Integrated Security=SSPI;database=Sullied;Server=.", ma => ma.MigrationsAssembly(migrationsAssembly));

            return new SulliedDbContext(optionsBuilder.Options);
        }
    }
}
