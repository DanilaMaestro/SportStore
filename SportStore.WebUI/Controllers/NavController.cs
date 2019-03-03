using SportStore.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        IProductsRepository _repository;

        public NavController(IProductsRepository rep)
        {
            _repository = rep;
        }

        // GET: Nav
        public PartialViewResult Menu()
        {
            var categories = _repository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c);


            return PartialView(categories);
        }
    }
}