using ApiCarSale.Models;
using ApiCarSale.Models.Dtos;
using AutoMapper;

namespace ApiCarSale.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CreateCarDto, Car>();
            CreateMap<UpdateCarDto, Car>();
        }
      
    }
}
