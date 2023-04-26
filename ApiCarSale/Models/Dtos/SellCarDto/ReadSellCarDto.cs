using MongoDB.Bson.Serialization.Attributes;

namespace ApiCarSale.Models.Dtos.SellCarDto
{
    public class ReadSellCarDto
    {
        public string Id { get; set; }
        [BsonElement("CarroId")]
        public string CarroId { get; set; }
        [BsonElement("Carro")]
        [BsonIgnoreIfNull]
        public Car Carro { get; set; }
        [BsonElement("ValorDesconto")]
        public double ValorDesconto { get; set; }
        [BsonElement("DataVenda")]
        public DateTime DataVenda { get; set; } = DateTime.Now;
    }
}
