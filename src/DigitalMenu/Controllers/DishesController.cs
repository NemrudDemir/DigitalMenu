using DigitalMenu.DataAccess.Abstractions;
using DigitalMenu.DataAccess.Builders;
using DigitalMenu.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishesController : ControllerBase
    {
        private const string _exampleDatabaseEndpointName = "ExampleData";
        private readonly IDigitalMenuRepository _repository;
        private readonly ILogger<DishesController> _logger;

        public DishesController(IDigitalMenuRepository repository, ILogger<DishesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Gets all dishes from database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dishes = await _repository.GetAllDishes();
            if (!dishes.Any())
            {
                var exampleDataUrl = Url.Link(_exampleDatabaseEndpointName, null);
                return NotFound($"No dishes found in mongodb. Use {exampleDataUrl} to reset the database with example dishes");
            }

            return Ok(dishes);
        }

        /// <summary>
        /// Removes all dishes and adds the example dishes to the database
        /// </summary>
        /// <returns></returns>
        [HttpPatch("example", Name = _exampleDatabaseEndpointName)]
        public async Task<IActionResult> InsertExamplesToMongo()
        {
            _logger.LogInformation("Datenbank wird zurückgesetzt.");
            await _repository.DeleteMany(_ => true); //delete all dishes
            var examples = GetExampleDishes();
            await _repository.InsertManyDishes(examples);
            return Ok(examples);
        }

        private IEnumerable<Dish> GetExampleDishes()
        {
            var breakfastStart = new TimeSpan(7, 0, 0); //ggf. auslagern und anpassen
            var breakfastEnd = new TimeSpan(11, 0, 0); //ggf. auslagern und anpassen
            yield return new Dish
            {
                Id = 1,
                Name = "Dish1",
                Description = "This is a short description for dish1",
                Price = 8.99m,
                ApproxWaitTime = TimeSpan.FromMinutes(20),
                Availibility = new AvailibilityBuilder().AddWeekdays().AddTimeStartEnd(breakfastStart, breakfastEnd).Create(),
                Category = DishCategory.MainCourse
            };

            yield return new Dish
            {
                Id = 2,
                Name = "Dish with nullValues"
            };

            yield return new Dish
            {
                Id = 3,
                Name = "Dish3",
                Price = 10m,
                IsDeactivated = true
            };
        }
    }
}
