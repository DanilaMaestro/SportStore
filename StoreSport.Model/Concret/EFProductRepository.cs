using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportStore.Model.Abstract;
using SportStore.Model.Entities;

namespace SportStore.Model.Concret
{
    public class EFProductRepository : IProductsRepository 
    {
        public EFProductRepository()
        {
            efContext = new EFDbContext();
        }
        EFDbContext efContext;

        public IQueryable<Product> Products {
            get
            {
                return efContext.Products;
            }
        }
    }
}
