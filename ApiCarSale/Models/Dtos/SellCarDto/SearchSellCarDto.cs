using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ApiCarSale.Models.Dtos.SellCarDto
{
    public class SearchSellCarDto
    {
        private const string DateFormat = "dd/MM/yyyy";

        public DateTime InitialDate { get; set; } = DateTime.ParseExact(DateTime.Now.ToString(DateFormat), DateFormat, CultureInfo.InvariantCulture);
        public DateTime FinalDate { get; set; } = DateTime.ParseExact(DateTime.Now.ToString(DateFormat), DateFormat, CultureInfo.InvariantCulture);
    }
}
