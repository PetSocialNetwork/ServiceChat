using AutoMapper;
using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Shared;
using ServiceChat.WebApi.Models.Requests;
using ServiceChat.WebApi.Models.Responses;

namespace ServiceChat.WebApi.Mappings
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageResponse>();
            CreateMap<PaginationRequest, PaginationOptions>();
        }
    }
}