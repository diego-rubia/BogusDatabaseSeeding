using BogusDatabaseSeeding.Model;
using Microsoft.EntityFrameworkCore;

namespace BogusDatabaseSeeding.Data
{
    public class EmployeeDbContext : DbContext
    {
        // Default constructor to prevent build errors
        public EmployeeDbContext()
        {
            
        }

        // Constructor with Options
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }
        
        // DbSet of based on the EmployeeModel, this is needed for Entity to generate the tables
        public DbSet<EmployeeModel> Employees { get; set; }

        // Specify the primary key for the EmployeeModel since it is required for database creation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>().HasKey(e => e.EmployeeID);
            base.OnModelCreating(modelBuilder);
        }

        // onConfigure override
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Build the database based on appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            // Get the connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("AppDb");
            optionsBuilder.UseSqlServer(connectionString);
        }

        

    }
}
