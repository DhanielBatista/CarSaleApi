using ApiCarSale.Dtos;
using ApiCarSale.Models;
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
