using AutoMapper;
using ComicLoreApi.Entities;
using ComicLoreApi.Models;
using System.Linq;

namespace ComicLoreApi.Profiles
{
    public class SupeProfile : Profile
    {
        public SupeProfile()
        {
            CreateMap<Supe, SupeWithPowersDto>()
                .ForMember(dest => dest.Powers, opt => opt.MapFrom(src => src.Powers.Select(p => p.Name)));
            CreateMap<SupeForUpdateDto, Supe>();
        }
    }
}
