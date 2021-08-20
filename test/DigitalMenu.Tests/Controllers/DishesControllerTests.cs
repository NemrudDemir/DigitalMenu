using DigitalMenu.Controllers;
using DigitalMenu.DataAccess.Abstractions;
using DigitalMenu.DataAccess.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DigitalMenu.Tests.Controllers
{
    public class DishesControllerTests
    {
        private Mock<IDigitalMenuRepository> _repoMock;
        private DishesController _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IDigitalMenuRepository>();
            _classUnderTest = new DishesController(_repoMock.Object, Mock.Of<ILogger<DishesController>>())
            {
                Url = Mock.Of<IUrlHelper>()
            };
        }

        [Test]
        public async Task GetWithNoDishesFoundShouldReturnNotFoundObjectResultWithString()
        {
            _repoMock.Setup(repo => repo.GetAllDishes()).ReturnsAsync(new Dish[0]);
            var result = await _classUnderTest.Get();

            result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().BeOfType<string>();
        }

        [Test]
        public async Task GetWithDishesFoundShouldReturnOkObjectResultWithDishes()
        {
            var exampleDishes = new[] { new Dish { Id = 20 } };
            _repoMock.Setup(repo => repo.GetAllDishes()).ReturnsAsync(exampleDishes);
            var result = await _classUnderTest.Get();

            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(exampleDishes);
        }

        [Test]
        public async Task InsertExamplesToMongoShouldDeleteManyAndInsertManyDishes()
        {
            var result = await _classUnderTest.InsertExamplesToMongo();

            _repoMock.Verify(repo => repo.DeleteMany(It.IsAny<Expression<Func<Dish, bool>>>()), Times.Once);
            _repoMock.Verify(repo => repo.InsertManyDishes(It.Is<IEnumerable<Dish>>(l => l.Any())), Times.Once);

            result.Should().BeAssignableTo<IStatusCodeActionResult>().Which.StatusCode.Should().Be(200);
        }
    }
}