using AutoMapper;
using sullied_data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace sullied_services.Models
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserEntity, User>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.Password, opt => opt.MapFrom(x => x.Password))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName));
        }
    }
}
