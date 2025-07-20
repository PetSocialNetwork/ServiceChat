using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceChat.Domain.Services;
using ServiceChat.Domain.Shared;
using ServiceChat.WebApi.Models.Requests;
using ServiceChat.WebApi.Models.Responses;

namespace ServiceChat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;
        private readonly IMapper _mapper;
        public MessageController(MessageService messageService,       
            IMapper mapper)
        {
            _messageService = messageService 
                ?? throw new ArgumentNullException(nameof(messageService));         
            _mapper = mapper 
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Удаляет сообщение по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сообщения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpDelete("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task DeleteMessageAsync
            ([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            await _messageService.DeleteMessageAsync(id, cancellationToken);
        }

        /// <summary>
        /// Возвращает все сообщения из чата
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<List<MessageResponse>> BySearchAsync
            ([FromBody] MessageRequest request, CancellationToken cancellationToken)
        {
            var options = _mapper.Map<PaginationOptions>(request.Options);
            var messages = await _messageService.BySearchAsync(request.ChatId, options, cancellationToken);
            return _mapper.Map<List<MessageResponse>>(messages);
        }
      
        /// <summary>
        /// Возвращает сообщение по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сообщения</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<MessageResponse> GetMessageByIdAsync
            ([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var message = await _messageService.GetMessageByIdAsync(id, cancellationToken);
            return _mapper.Map<MessageResponse>(message);
        }

        /// <summary>
        /// Обновляет сообщение по идентификатору
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPut("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task UpdateMessageAsync
            ([FromBody] UpdateMessageRequest request, CancellationToken cancellationToken)
        {
            await _messageService.UpdateMessageAsync(request.Id, request.MessageText, cancellationToken);
        }

        /// <summary>
        /// Возвращает последнее сообщение в чате
        /// </summary>
        /// <param name="chatId">Идентификатор чата</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<MessageResponse?> GetLastMessageByChatIdAsync
            ([FromQuery] Guid chatId, CancellationToken cancellationToken)
        {
            var message = await _messageService.GetLastMessageByChatIdAsync(chatId, cancellationToken);
            return message != null
                ? _mapper.Map<MessageResponse>(message)
                : new MessageResponse();
        }
    }
}
