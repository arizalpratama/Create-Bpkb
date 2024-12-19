using BpkbApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BpkbApi.Data
{
    public class BpkbDbContext : DbContext
    {
        public BpkbDbContext(DbContextOptions<BpkbDbContext> options) : base(options) { }

        public DbSet<MsUser> MsUsers { get; set; }
        public DbSet<MsStorageLocation> MsStorageLocations { get; set; }
        public DbSet<TrBpkb> TrBpkbs { get; set; }
    }
}
