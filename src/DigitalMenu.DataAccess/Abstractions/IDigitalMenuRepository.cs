using DigitalMenu.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DigitalMenu.DataAccess.Abstractions
{
    public interface IDigitalMenuRepository
    {
        Task<IEnumerable<Dish>> GetAllDishes();

        Task InsertManyDishes(IEnumerable<Dish> dishes);

        Task DeleteMany(Expression<Func<Dish, bool>> filterExpression);
    }
}
