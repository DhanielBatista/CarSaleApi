using ApiCarSale.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiCarSale.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> _carCollection;

        public CarService(IOptions<CarSaleDatabaseSettings> carSaleDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                carSaleDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                carSaleDatabaseSettings.Value.DatabaseName);
            _carCollection = mongoDatabase.GetCollection<Car>(
                carSaleDatabaseSettings.Value.CarCollectionName);
        }

        public async Task<List<Car>> GetAsync() => 
            await _carCollection.Find(x => true).ToListAsync();
        public async Task<Car?> GetAsync(string id) => 
            await _carCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Car newCar) =>
            await _carCollection.InsertOneAsync(newCar);
        public async Task UpdateAsync(string id, Car updatedCar) =>
            await _carCollection.ReplaceOneAsync(x => x.Id == id, updatedCar);
        public async Task DeleteAsync(string id) =>
            await _carCollection.DeleteOneAsync(x => x.Id == id);

    }
}
