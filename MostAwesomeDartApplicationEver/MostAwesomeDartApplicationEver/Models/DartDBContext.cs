using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace MostAwesomeDartApplicationEver.Models
{
    internal class DartDbContext : DbContext
    {
        public DbSet<Darter> Darters { get; set; }

        public DbSet<Match> Matches { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=dartdata.db");
        }

        private static DartDbContext? _context;
        public static DartDbContext Context
        {
            get
            {
                if (_context is null)
                {
                    _context = new DartDbContext();
                    _context.Database.EnsureCreated();
                }
                return _context;
            }
        }
    }
}
