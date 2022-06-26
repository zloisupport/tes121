using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tes121
{
     public class AppDbContext : DbContext
    {
        public AppDbContext() :
        base("defaultConnect")
        { }
        public DbSet<tblDolzh> tblDolzhs { get; set; }
    }
}
