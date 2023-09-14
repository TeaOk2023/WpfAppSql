using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppSql1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Car> Cars { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=carbase.db");
        }
    }

}
