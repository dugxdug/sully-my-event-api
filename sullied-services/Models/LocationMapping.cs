using AutoMapper;
using sullied_data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace sullied_services.Models
{
    public class LocationMapping : Profile
    {
        public LocationMapping()
        {
            CreateMap<LocationEntity, Location>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.YelpId, opt => opt.MapFrom(x => x.YelpId))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Rating))
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.ImageUrl))
                .ForMember(x => x.Url, opt => opt.MapFrom(x => x.Url))
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Address));
        }
    }
}
