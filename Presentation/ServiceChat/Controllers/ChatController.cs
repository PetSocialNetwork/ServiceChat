using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Services;
using ServiceChat.WebApi.Models.Requests;
using ServiceChat.WebApi.Models.Responses;

namespace ServiceChat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly MessageService _messageService;
        private readonly IMapper _mapper;
        public ChatController(ChatService chatService, MessageService messageService, IMapper mapper)
        {
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpDelete("[action]")]
        public async Task DeleteChatAsync([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            //Транзакция
            await _chatService.DeleteChatAsync(id, cancellationToken);
            await _messageService.DeleteAllMessagesByChatIDAsync(id, cancellationToken);
        }

        [HttpPost("[action]")]
        public async Task<AddChatResponse> AddChatAsync([FromBody] AddChatRequest request, CancellationToken cancellationToken)
        {
            var chat = _mapper.Map<Chat>(request);
            var addedChat = await _chatService.AddChatAsync(chat, cancellationToken);
            return _mapper.Map<AddChatResponse>(addedChat);
        }

        [HttpPost("[action]")]
        public async Task<List<ChatResponse>> BySearchAsync([FromBody] ChatRequest request, CancellationToken cancellationToken)
        {
            var chats = await _chatService.BySearchAsync(request.UserId, request.Take, request.Offset, cancellationToken);
            return _mapper.Map<List<ChatResponse>>(chats);
        }

        [HttpGet("[action]")]
        public async Task<AddChatResponse> GetChatByIdAsync([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var chat = await _chatService.GetChatByIdAsync(id,cancellationToken);
            return _mapper.Map<AddChatResponse>(chat);
        }

        [HttpPost("[action]")]
        public async Task<AddChatResponse> GetOrCreateChatAsync([FromBody] AddChatRequest request, CancellationToken cancellationToken)
        {
            var chat = _mapper.Map<Chat>(request);
            var addedChat = await _chatService.GetOrCreateChatAsync(chat, cancellationToken);
            return _mapper.Map<AddChatResponse>(addedChat);
        }
    }
}
