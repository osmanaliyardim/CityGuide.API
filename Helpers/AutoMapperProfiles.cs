using AutoMapper;
using CityGuide.API.DTOs;
using CityGuide.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityGuide.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityForListDto>()
                .ForMember(destination => destination.PhotoUrl, option =>
                {
                    option.MapFrom(source => source.Photos.FirstOrDefault(x => x.IsMain).Url);
                });

            CreateMap<City, CityForDetailDto>();
        }
    }
}
