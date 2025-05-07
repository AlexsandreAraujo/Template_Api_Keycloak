using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using TemplateApi.Model;

namespace TemplateApi.Infraenstrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
                "Server =.; " +
                "Database=TemplateApi; " +
                "Trusted_Connection=true; " +
                "MultipleActiveResultSets=true; " +
                "Integrated Security = true; " +
                "TrustServerCertificate=True");
    }
}
