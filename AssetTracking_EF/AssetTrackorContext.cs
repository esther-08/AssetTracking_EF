using Microsoft.EntityFrameworkCore;

namespace AssetTracking_EF
{
    class AssetTrackorContext : DbContext
    {
        public DbSet<CompanyAsset> CompanyAssets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-Q3LBSV4\SQLEXPRESS;Initial Catalog=AssetTrackor;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
    
}
