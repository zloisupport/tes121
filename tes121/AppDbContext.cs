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
        base("BookStoreContext")
        { }

       public DbSet<Category> Categories { get; set; }
       public DbSet<Region> Regions { get; set; }
       public DbSet<Orders> Orders { get; set; }
       public DbSet<Sotrudnik> Sotrudniks { get; set; }
       public DbSet<Dolzh> Dolzhs { get; set; }

    }
}
