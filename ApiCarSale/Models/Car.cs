﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ApiCarSale.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("Modelo")]
        public string? Modelo { get; set; }
        [BsonElement("Marca")]
        public string? Marca { get; set; }
        [BsonElement("Ano")]
        public int Ano { get; set; }
        [BsonElement("Preco")]
        public double Preco { get; set; }
        [BsonElement("DataCadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        [BsonElement("CarroVenda")]
        public bool CarroVendido { get; set; } = false;


    }
}
