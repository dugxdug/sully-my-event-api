using AutoMapper;
using sullied_data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace sullied_services.Models
{
    public class EventMapping : Profile
    {
        public EventMapping()
        {
            CreateMap<EventEntity, Event>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.LocationId, opt => opt.MapFrom(x => x.LocationId))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
                .ForMember(x => x.CreatedById, opt => opt.MapFrom(x => x.User.Id))
                .ForMember(x => x.EventTime, opt => opt.MapFrom(x => x.Time))
                .ForMember(x => x.SelectedLocations, opt => opt.Ignore())
                .ForMember(x => x.EventLocations, opt => opt.MapFrom(x => x.EventLocations));
        }
    }
}
