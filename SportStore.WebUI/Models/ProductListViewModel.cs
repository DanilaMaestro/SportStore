using SportStore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportStore.WebUI.Models
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public PagingInfo PageInfo { get; set; }
    }
}