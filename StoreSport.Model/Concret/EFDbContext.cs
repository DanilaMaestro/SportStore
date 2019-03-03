using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SportStore.Model.Entities;

namespace SportStore.Model.Concret
{
    public class EFDbContext:DbContext
    {
        public EFDbContext()
        {
            Products = this.Set<Product>();
        }
        public DbSet<Product> Products { get; }
    }
}
