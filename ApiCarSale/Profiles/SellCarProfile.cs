using ApiCarSale.Models;
using ApiCarSale.Models.Dtos.SellCarDto;
using AutoMapper;

namespace ApiCarSale.Profiles
{
    public class SellCarProfile : Profile
    {
        public SellCarProfile()
        {
            CreateMap<CreateSellCarDto, SellCar>();
            CreateMap<UpdateSellCarDto, SellCar>();
        }
    }
}
