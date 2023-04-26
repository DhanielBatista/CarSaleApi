namespace ApiCarSale.Models
{
    public class CarSaleDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CarCollectionName { get; set; } = null!;
        public string SellCarCollectionName { get; set; } = null!;

    }
}
