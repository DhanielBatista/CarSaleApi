using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiCarSale.Models.Dtos.SellCarDto
{
    public class CreateSellCarDto
    {
        public string CarroId { get; set; }
        public double ValorDesconto { get; set; }
    }
}
