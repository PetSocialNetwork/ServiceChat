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

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ChatNotFoundException))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("[action]")]
        public async Task DeleteChatAsync([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            //Транзакция
            await _chatService.DeleteChatAsync(id, cancellationToken);
            await _messageService.DeleteAllMessagesByChatIDAsync(id, cancellationToken);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("[action]")]
        public async Task<AddChatResponse> AddChatAsync([FromBody] AddChatRequest request, CancellationToken cancellationToken)
        {
            var chat = _mapper.Map<Chat>(request);
            var addedChat = await _chatService.AddChatAsync(chat, cancellationToken);
            return _mapper.Map<AddChatResponse>(addedChat);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("[action]")]
        public async Task<List<ChatResponse>> BySearchAsync([FromQuery] Guid userId, CancellationToken cancellationToken)
        {
            var chats = await _chatService.BySearchAsync(userId, cancellationToken);
            return _mapper.Map<List<ChatResponse>>(chats);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageFoundException))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("[action]")]
        public async Task<AddChatResponse> GetChatByIdAsync([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            //сделать с Include Messages
            var chat = await _chatService.GetChatByIdAsync(id,cancellationToken);
            return _mapper.Map<AddChatResponse>(chat);
        }      
    }
}
