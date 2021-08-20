using DigitalMenu.DataAccess.Entities;
using MongoDB.Driver;

namespace DigitalMenu.DataAccess.Abstractions
{
    public interface IDigitalMenuContext
    {
        IMongoCollection<Dish> Dishes { get; }
    }
}
