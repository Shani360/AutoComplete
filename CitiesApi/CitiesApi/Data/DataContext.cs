using Microsoft.EntityFrameworkCore;

namespace CitiesApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option) { }

        //public DbSet<City> Cities { get; set; }
        public DbSet<City> Cities => Set<City>();
        public virtual IQueryable<City> GetCities() => Cities.AsQueryable();

    }
}
