namespace ApiCarSale.Models.Dtos.CarDto
{
    public class CreateCarDto
    {
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public int Ano { get; set; }
        public double Preco { get; set; }
    }
}
