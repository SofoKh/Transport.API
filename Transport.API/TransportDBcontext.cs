using Microsoft.EntityFrameworkCore;

namespace Vehicles0802
{
    public partial class TransportDBcontext:DbContext
    {
        public TransportDBcontext()
        {
        }

        public TransportDBcontext(DbContextOptions<TransportDBcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }

        public virtual DbSet<Transport> Vehicles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Server = (local); Database = VehiclesAPI; Trusted_Connection = True; ");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
