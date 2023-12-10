using Microsoft.EntityFrameworkCore;

namespace efCoreApp.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {            
        }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Kurs> Kurslar { get; set; } 
        public DbSet<KursKayit> KursKayitlari { get; set; }
    }
}