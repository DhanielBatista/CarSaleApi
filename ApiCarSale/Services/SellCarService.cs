using ApiCarSale.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiCarSale.Services
{
    public class SellCarService
    {
        private readonly IMongoCollection<SellCar> _sellCarCollection;
        private readonly IAggregateFluent<SellCar> _sellCarAgregation;
        private readonly IMongoCollection<Car> _carCollection;
        public SellCarService(IOptions<CarSaleDatabaseSettings> carSaleDatabaseSettings)
        {

            var mongoClient = new MongoClient(
               carSaleDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                carSaleDatabaseSettings.Value.DatabaseName);
            _carCollection = mongoDatabase.GetCollection<Car>(
                carSaleDatabaseSettings.Value.CarCollectionName);
            _sellCarCollection = mongoDatabase.GetCollection<SellCar>(
                carSaleDatabaseSettings.Value.SellCarCollectionName);


        }

        public async Task<List<SellCar>> GetAsync() =>
            await _sellCarCollection.Find(x => true).ToListAsync();
        public async Task<SellCar?> GetAsync(string id) =>
            await _sellCarCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(SellCar newSellCar) =>
            await _sellCarCollection.InsertOneAsync(newSellCar);
        public async Task UpdateAsync(string id, SellCar updateSellCar) =>
            await _sellCarCollection.ReplaceOneAsync(x => x.Id == id, updateSellCar);
        public async Task DeleteAsync(string id) =>
            await _sellCarCollection.DeleteOneAsync(x => x.Id == id);


    }
}
