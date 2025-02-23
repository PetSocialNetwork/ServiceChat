using AutoMapper;
using ServiceChat.Domain.Entities;
using ServiceChat.WebApi.Models.Responses;

namespace ServiceChat.WebApi.Mappings
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageResponse>();
        }
    }
}