using Microsoft.AspNetCore.Mvc;
using TvStore.Controllers;
using TvStore.Models;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TvStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfTvs()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestTvs());
            var controller = new HomeController(mock.Object);

            // Act
            var result = controller.Index();

            // Assert
            // Является ли возвращаемый результат объектом ViewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            // Передается ли в представление в качестве модели объект IEnumerable<Tv>
            var model = Assert.IsAssignableFrom<IEnumerable<Tv>>(
                viewResult.Model);
            // проверяется количество объектов, которые передаются в представление
            Assert.Equal(GetTestTvs().Count, model.Count());
        }
        private List<Tv> GetTestTvs()
        {
            var tvs = new List<Tv>
            {
                new Tv { Id = 1, Name = "Sony ABC400", Price = 35000, Company = "Sony", Size = 40},
                new Tv { Id = 2, Name = "Sharp x234", Price = 25000, Company = "Sharp", Size = 25},
                new Tv { Id = 3, Name = "Panasonic p1023", Price = 33000, Company = "Panasonic", Size = 32},
                new Tv { Id = 3, Name = "Rubin s930", Price = 19000, Company = "Rubin", Size = 19},
            };
            return tvs;
        }
        [Fact]
        public void AddTvReturnsViewResultWithTvModel()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var controller = new HomeController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            Tv newTv = new Tv();

            // Act
            var result = controller.AddTv(newTv);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newTv, viewResult?.Model);
        }

        [Fact]
        public void AddTvReturnsARedirectAndAddsTv()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var controller = new HomeController(mock.Object);
            var newTv = new Tv()
            {
                Name = "Sony"
            };

            // Act
            var result = controller.AddTv(newTv);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(r => r.Create(newTv));
        }

        [Fact]
        public void GetTvReturnsBadRequestResultWhenIdIsNull()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var controller = new HomeController(mock.Object);

            // Act
            var result = controller.GetTv(null);

            // Arrange
            // Является ли возвращаемый результат объектом ViewResult?
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void GetTvReturnsNotFoundResultWhenTvNotFound()
        {
            // Arrange
            int testTvId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.Get(testTvId))
                .Returns(null as Tv);
            var controller = new HomeController(mock.Object);

            // Act
            var result = controller.GetTv(testTvId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetTvReturnsViewResultWithTv()
        {
            // Arrange
            int testTvId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.Get(testTvId))
                .Returns(GetTestTvs().FirstOrDefault(p => p.Id == testTvId));
            var controller = new HomeController(mock.Object);

            // Act
            var result = controller.GetTv(testTvId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Tv>(viewResult.ViewData.Model);
            Assert.Equal("Sony", model.Company);
            Assert.Equal(testTvId, model.Id);
            Assert.Equal(35000, model.Price);
            Assert.IsType<int>(model.Size);
            Assert.IsType<string>(model.Name);
            Assert.IsType<decimal>(model.Price);
            Assert.Contains("Sony ABC400", model.Name);
            Assert.Contains("Sony", model.Company);
        }
    }
}

