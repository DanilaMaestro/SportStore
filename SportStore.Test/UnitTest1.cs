using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.CSharp;
using System.Web;
using System.Web.Mvc;
using SportStore.WebUI.Controllers;
using SportStore.Model.Abstract;
using SportStore.Model.Entities;
using System.Linq;
using SportStore.WebUI.Models;
using SportStore.WebUI.Helpers;
using System.Collections;
using System.Collections.Generic;

namespace SportStore.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanGenerateCategory()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(
                new[] {
                    new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                    new Product { ProductID = 2, Name = "P2", Category = "Apples" },
                    new Product { ProductID = 3, Name = "P3", Category = "Plum" },
                    new Product { ProductID = 4, Name = "P4", Category = "Oranges" }
                }.AsQueryable()
                );

            NavController navController = new NavController(mock.Object);
            string[] result = ((IEnumerable<string>)(navController.Menu().Model)).ToArray();

            Assert.AreEqual(3, result.Length);

            Assert.AreEqual("Apples", result[0]);
            Assert.AreEqual("Oranges", result[1]);
            Assert.AreEqual("Plum", result[2]);

        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product{ ProductID = 1, Name = "P1" },
                    new Product{ ProductID = 2, Name = "P2" },
                    new Product{ ProductID = 3, Name = "P3" },
                    new Product{ ProductID = 4, Name = "P4" },
                    new Product{ ProductID = 5, Name = "P5" }
                }.AsQueryable());

            ProductController productController = new ProductController(mock.Object);
            productController.PageSize = 3;

            var ViewModel = (ProductListViewModel)productController.List(null, 2).Model;

            Assert.AreEqual(3, ViewModel.PageInfo.ItemPerPage);
            Assert.AreEqual(2, ViewModel.PageInfo.CurrentPage);
            Assert.AreEqual(5, ViewModel.PageInfo.ItemsCount);
            Assert.AreEqual(2, ViewModel.PageInfo.PageCount);
        }

        [TestMethod]
        public void Pagenit()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product{ ProductID = 1, Name = "P1" },
                    new Product{ ProductID = 2, Name = "P2" },
                    new Product{ ProductID = 3, Name = "P3" },
                    new Product{ ProductID = 4, Name = "P4" },
                    new Product{ ProductID = 5, Name = "P5" }
                }.AsQueryable());

            ProductController productController = new ProductController(mock.Object);
            productController.PageSize = 3;

            ProductListViewModel ViewModel = (ProductListViewModel)productController.List(null, 2).Model;
            Product[] result = ViewModel.Products.ToArray();

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("P4", result[0].Name);
            Assert.AreEqual("P5", result[1].Name);
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;

            PagingInfo info = new PagingInfo();
            info.ItemsCount = 28;
            info.CurrentPage = 2;
            info.ItemPerPage = 10;

            Func<int, string> urlFunc = i => "Page" + i;

            var result = myHelper.HtmlLinks(info, urlFunc);

            Assert.AreEqual(@"<a href=""Page1"">1</a>"
                            + @"<a class=""selected"" href=""Page2"">2</a>"
                            + @"<a href=""Page3"">3</a>" , result.ToString());
        }

        [TestMethod]
        public void CanCategoryShow()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]
                {
                    new Product {ProductID = 1, Name = "P1", Category = "Cat1" },
                    new Product {ProductID = 2, Name = "P2", Category = "Cat2" },
                    new Product {ProductID = 3, Name = "P3", Category = "Cat1" },
                    new Product {ProductID = 4, Name = "P4", Category = "Cat2" },
                    new Product {ProductID = 5, Name = "P5", Category = "Cat3" }
                }.AsQueryable()
                );

            ProductController productController = new ProductController(mock.Object);
            productController.PageSize = 3;

            Product[] products = ((ProductListViewModel)productController.List(category: "Cat2").Model).Products.ToArray();

            Assert.AreEqual(2, products.Length);
            Assert.IsTrue(products[0].Name == "P2" && products[0].Category == "Cat2");
            Assert.IsTrue(products[1].Name == "P4" && products[1].Category == "Cat2");
        }

    }
}
