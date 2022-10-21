using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MostAwesomeDartApplicationEver.Models
{
    internal class DartDbContext : DbContext
    {
        public DbSet<Darter> Darters { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=dartdata.db");
        }
    }
}
