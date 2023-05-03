using ApiCarSale.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiCarSale.Services
{
    public class SellCarService
    {
        private readonly IMongoCollection<SellCar> _sellCarCollection;
        private readonly PipelineDefinition<SellCar, SellCar> _pipeline;
        private readonly IMongoCollection<Car> _carCollection;
        public SellCarService(IOptions<CarSaleDatabaseSettings> carSaleDatabaseSettings)
        {

            var mongoClient = new MongoClient(carSaleDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(carSaleDatabaseSettings.Value.DatabaseName);

            _carCollection = mongoDatabase.GetCollection<Car>(carSaleDatabaseSettings.Value.CarCollectionName);
            _sellCarCollection = mongoDatabase.GetCollection<SellCar>(carSaleDatabaseSettings.Value.SellCarCollectionName);

            _pipeline = new BsonDocument[]
            {
                new BsonDocument("$lookup",
                    new BsonDocument
                    {
                        { "from", "Car" },
                        { "localField", "CarroId" },
                        { "foreignField", "_id" },
                        { "as", "Carro" },
                    }),
                new BsonDocument("$set",
                    new BsonDocument
                    {
                        { "Carro", new BsonDocument("$first", "$Carro") },
                    })
            };
        }
        public async Task<List<SellCar>> GetByDateAsync(DateTime initialDate, DateTime finalDate)
        {
            var filter = Builders<SellCar>.Filter.Gte(x => x.DataVenda, initialDate) &
                         Builders<SellCar>.Filter.Lte(x => x.DataVenda, finalDate);

            return await _sellCarCollection.Find(filter).ToListAsync();
        }
        public async Task<List<SellCar>> GetAsync()
        {
            using var agregation = _sellCarCollection.Aggregate(_pipeline);
            return await agregation.ToListAsync();
        }
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
