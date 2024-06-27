using AutoMapper;
using ComicLoreApi.Entities;
using ComicLoreApi.Models;

namespace ComicLoreApi.Profiles
{
    public class PowerProfile : Profile
    {
        public PowerProfile()
        {
            CreateMap<PowerDto, Power>();
            CreateMap<Power, PowerDto>();
        }
    }
}
