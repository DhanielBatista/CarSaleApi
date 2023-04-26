using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiCarSale.Models
{
    public class SellCar
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Carro")]
        public Car Carro { get; set; }
        [BsonElement("ValorDesconto")]
        public double ValorDesconto { get; set; }
        [BsonElement("DataVenda")]
        public DateTime DataVenda{ get; set; } = DateTime.Now;
         [BsonElement("ValorVenda")]
        public double ValorVenda
        {
            get
            {
                return Carro.Preco - ValorDesconto;
            }
           
        }
    }
}
