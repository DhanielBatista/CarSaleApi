using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ApiCarSale.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Modelo")]
        public string? Modelo { get; set; }
        public string? Marca { get; set; } 
        public int Ano { get; set; }
        public double Preco { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;

    }
}
