using DigitalMenu.DataAccess.Abstractions;
using DigitalMenu.DataAccess.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DigitalMenu.DataAccess.Repositories
{
    public class DigitalMenuRepository : IDigitalMenuRepository
    {
        private readonly IDigitalMenuContext _context;

        public DigitalMenuRepository(IDigitalMenuContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dish>> GetAllDishes()
        {
            return await _context.Dishes.Find(_ => true).ToListAsync();
        }

        public async Task InsertManyDishes(IEnumerable<Dish> dishes)
        {
            await _context.Dishes.InsertManyAsync(dishes);
        }

        public async Task DeleteMany(Expression<Func<Dish, bool>> filterExpression)
        {
            await _context.Dishes.DeleteManyAsync(filterExpression);
        }
    }
}
