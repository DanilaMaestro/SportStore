using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStore.Model.Abstract;
using SportStore.WebUI.Models;
using SportStore.Model.Entities;

namespace SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository _productsRepository;
        public int PageSize { get; set; } = 4;

        public ProductController(IProductsRepository repository)
        {
            _productsRepository = repository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel productList = new ProductListViewModel();

            productList.Products = _productsRepository.Products
                .Where(p => p.Category == category || category == null)
                .OrderBy(p => p.ProductID)
                .Skip(PageSize * (page - 1))
                .Take(PageSize).ToList();

            productList.PageInfo = new PagingInfo
            {
                ItemsCount = _productsRepository.Products.Count(),
                ItemPerPage = PageSize,
                CurrentPage = page,
                CurrentCategory = category
            };


            return View(productList); 
        }
    }
}