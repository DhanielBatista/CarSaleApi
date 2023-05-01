using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace ApiCarSale.Models
{
    public class SellCar
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("CarroId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CarroId { get; set; }
        public Car? Carro { get; set; }
        [BsonElement("ValorDesconto")]
        public double ValorDesconto { get; set; }
        [BsonElement("ValorVenda")]
        public double ValorVenda { get { return CalculoVenda(); } }

        [BsonElement("DataVenda")]
        public DateTime DataVenda { get; set; } = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy")); 

        public double CalculoVenda()
        {
          return  Carro.Preco - ValorDesconto;
        }

    }
}
