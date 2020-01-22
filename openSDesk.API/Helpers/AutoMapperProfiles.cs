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
            // application and authorization specific mappings
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

            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.SenderPhotoUrl, opt => opt
                    .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain == true).Url))
                .ForMember(m => m.RecipientPhotoUrl, opt => opt
                    .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain == true).Url));
            
            // bussines logic specific mappings
            CreateMap<TicketForAddDto, Ticket>()
                .ForMember(t => t.CreatedAt, opt => opt.MapFrom(dto => DateTime.Now))
                .ForMember(t => t.ModifiedAt, opt => opt.MapFrom(dto => DateTime.Now))
                .ForMember(t => t.Deleted, opt => opt.Equals(false));
            CreateMap<Ticket, TicketForDetailedDto>()
                .ForMember(dto => dto.Source, opt => opt.MapFrom(t => t.Source.Text));
                //.ForMember(dto => dto.Status, opt => opt.MapFrom(t => t.Status.Text))
                //.ForMember(dto => dto.SubStatus, opt => opt.MapFrom(t => t.SubStatus.Text))
                //.ForMember(dto => dto.Category, opt => opt.MapFrom(t => t.Category.Text))
                //.ForMember(dto => dto.Requester, opt => opt.MapFrom(t => t.Requester.Username))
                //.ForMember(dto => dto.AssignmentGroup, opt => opt.MapFrom(t => t.AssignmentGroup.Name))
                //.ForMember(dto => dto.AssignedTo, opt => opt.MapFrom(t => t.AssignedTo.Username));
            CreateMap<Note, NoteForDetailedDto>()
                .ForMember(dto => dto.Owner, opt => opt.MapFrom(n => n.Owner.Username));
            CreateMap<Resolution, ResolutionForDetailedDto>()
                .ForMember(dto => dto.Code, opt => opt.MapFrom(r => r.Code.Text))
                .ForMember(dto => dto.Owner, opt => opt.MapFrom(r => r.Owner.Username));
            CreateMap<Survey, SurveyForDetailedDto>()
                .ForMember(dto => dto.Response, opt => opt.MapFrom(s => s.Response.Text))
                .ForMember(dto => dto.Response, opt => opt.MapFrom(s => s.Response.Refusal));
            CreateMap<ResolutionForAddDto, Resolution>();
            CreateMap<SurveyForAddDto, Survey>();
            CreateMap<UserGroup, UserGroupForTicketDetailDto>();
            CreateMap<User, UserForTicketDetailDto>();
        }
    }
}