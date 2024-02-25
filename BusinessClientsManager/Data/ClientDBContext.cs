using BusinessClientsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessClientsManager.Data
{
    public class ClientDBContext : DbContext
    {
        public ClientDBContext(DbContextOptions<ClientDBContext> options) : base(options)
        {
        }

        public DbSet<BusinessClient> BusinessClients { get; set; }
        public DbSet<Postcode> Postcodes { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
