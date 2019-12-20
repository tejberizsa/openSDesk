using System;
using System.Linq;
using AutoMapper;
using openSDesk.API.Dtos;
using openSDesk.API.Models;

namespace openSDesk.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>()
                .ForMember(u => u.EMailConfirmed, opt => opt.Equals(false))
                .ForMember(u => u.Deleted, opt => opt.Equals(false));
            CreateMap<User, UserForListDto>()
                .ForMember(u => u.PhotoUrl, opt => opt.MapFrom(p => p.Photos.FirstOrDefault(ph => ph.IsMain == true).Url));
            CreateMap<User, UserForDetailedDto>()
                .ForMember(u => u.PhotoUrl, opt => opt.MapFrom(p => p.Photos.FirstOrDefault(ph => ph.IsMain == true).Url));
            CreateMap<UserForUpdateDto, User>()
                .ForMember(u => u.EMailConfirmed, opt => opt.Equals(false))
                .ForMember(u => u.Deleted, opt => opt.Equals(false));

            
            CreateMap<UserPhoto, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, UserPhoto>();

        }
    }
}