using Microsoft.EntityFrameworkCore;

namespace bakimonarim.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Yonetici> Yoneticiler { get; set; }
        public virtual DbSet<Urun> Urunler { get; set; }
        public virtual DbSet<Kategori> Kategoriler { get; set; }


    }
}
