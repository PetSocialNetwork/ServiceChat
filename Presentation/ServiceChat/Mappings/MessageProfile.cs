using AutoMapper;
using ServiceChat.Domain.Entities;
using ServiceChat.WebApi.Models.Requests;
using ServiceChat.WebApi.Models.Responses;

namespace ServiceChat.WebApi.Mappings
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageResponse>();

            CreateMap<AddMessageRequest, Message>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.MessageText, opt => opt.MapFrom(src => src.MessageText))
                .ForMember(dest => dest.DateRecord, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}