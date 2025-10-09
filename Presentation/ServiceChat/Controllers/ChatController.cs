using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Services;
using ServiceChat.Domain.Shared;
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
        public ChatController(ChatService chatService, 
            MessageService messageService, 
            IMapper mapper)
        {
            _chatService = chatService 
                ?? throw new ArgumentNullException(nameof(chatService));
            _messageService = messageService 
                ?? throw new ArgumentNullException(nameof(messageService));
            _mapper = mapper 
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Удаляет чат по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор чата</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpDelete("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task DeleteChatAsync
            ([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            await _chatService.DeleteChatAsync(id, cancellationToken);
            await _messageService.DeleteAllMessagesByChatIdAsync(id, cancellationToken);
        }

        /// <summary>
        /// Добавляет новый чат
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<AddChatResponse> AddChatAsync
            ([FromBody] AddChatRequest request, CancellationToken cancellationToken)
        {
            var chat = _mapper.Map<Chat>(request);
            var addedChat = await _chatService.AddChatAsync(chat, cancellationToken);
            return _mapper.Map<AddChatResponse>(addedChat);
        }

        /// <summary>
        /// Возвращает все чаты по идентификатору пользователя
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<List<ChatResponse>> BySearchAsync
            ([FromBody] ChatRequest request, CancellationToken cancellationToken)
        {
            var options = _mapper.Map<PaginationOptions>(request.Options);
            var chats = await _chatService.BySearchAsync((Guid)request.UserId, options, cancellationToken);
            return _mapper.Map<List<ChatResponse>>(chats);
        }

        /// <summary>
        /// Возвращает чат по идентифкатору
        /// </summary>
        /// <param name="id">Идентификатор чата</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<AddChatResponse> GetChatByIdAsync
            ([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var chat = await _chatService.GetChatByIdAsync(id,cancellationToken);
            return _mapper.Map<AddChatResponse>(chat);
        }

        /// <summary>
        /// Получает имеющийся чат, либо создает
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<AddChatResponse> GetOrCreateChatAsync
            ([FromBody] AddChatRequest request, CancellationToken cancellationToken)
        {
            var chat = _mapper.Map<Chat>(request);
            var addedChat = await _chatService.GetOrCreateChatAsync(chat, cancellationToken);
            return _mapper.Map<AddChatResponse>(addedChat);
        }
    }
}
