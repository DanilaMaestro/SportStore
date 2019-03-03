using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using SportStore.Model.Abstract;
using SportStore.Model.Entities;
using SportStore.Model.Concret;
using Ninject;
using Moq;

namespace SportStore.WebUI.Infrastructure
{

    public class NinjectControllerFactory : DefaultControllerFactory  
    {
        IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        void AddBindings()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product { Name = "Footbol", Price = 75 },
                new Product { Name = "Surface board", Price = 159 },
                new Product { Name = "Running scope", Price = 92 }
            }.AsQueryable());

            _ninjectKernel.Bind<IProductsRepository>().To(typeof(EFProductRepository));
        }

        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            return (controllerType == null) ? null : (IController)_ninjectKernel.Get(controllerType);
        }
    }
}