using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Module.Introduction.Infrastructure;
using Module.Introduction.Models;
using Module.Introduction.Services;
using Moq;
using NUnit.Framework;

namespace TestsProject
{
    public class ProductsControllerTests
    {
        [Test]
        public async Task GetDetails_ReturnsNotNullResult()
        {
            var contextMock = new Mock<NorthwindContext>();
            var productsServiceMock = new Mock<IProductsService>();
            var applicationSettingsMock = new Mock<IOptions<ApplicationSettings>>();
            productsServiceMock
                .Setup(service => service.GetDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(() => new Products { ProductName= "TestProducts" });
            var categoriesController = new Module.Introduction.Controllers.ProductsController(contextMock.Object, applicationSettingsMock.Object, productsServiceMock.Object);
            var result = await categoriesController.Details(1);

            Assert.IsNotNull(result);
        }
    }
}
