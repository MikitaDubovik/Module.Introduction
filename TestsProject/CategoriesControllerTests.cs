using System.Threading.Tasks;
using Module.Introduction.Models;
using Module.Introduction.Services;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class CategoriesController
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetDetails_ReturnsNotNullResult()
        {
            var categoriesServiceMock = new Mock<ICategoriesService>();
            categoriesServiceMock
                .Setup(service => service.GetDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(() => new Categories { CategoryName = "TestCategories" });
            var categoriesController = new Module.Introduction.Controllers.CategoriesController( categoriesServiceMock.Object);
            var result = await categoriesController.Details(1);

            Assert.IsNotNull(result);
        }
    }
}