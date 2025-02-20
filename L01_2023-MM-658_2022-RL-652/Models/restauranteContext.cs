using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2023_MM_658_2022_RL_652.Models
{
    public class RestauranteContext : DbContext
    {
        public RestauranteContext(DbContextOptions<RestauranteContext> options) : base(options)
        {

        }
        public DbSet<clientes> clientes { get; set; }
        public DbSet<pedidos> pedidos { get; set; }
        public DbSet<platos> platos { get; set; }
        public DbSet<motoristas> motoristas { get; set; }

    }
}
