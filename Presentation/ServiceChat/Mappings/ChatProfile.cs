using AutoMapper;
using ServiceChat.Domain.Entities;
using ServiceChat.WebApi.Models.Requests;
using ServiceChat.WebApi.Models.Responses;

namespace ServiceChat.WebApi.Mappings
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<AddChatRequest, Chat>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FriendIds, opt => opt.MapFrom(src => src.FriendIds))
                .ForMember(dest => dest.Messages, opt => opt.Ignore());

            CreateMap<Chat, AddChatResponse>();
            CreateMap<Chat, ChatResponse>();


        }
    }
}
