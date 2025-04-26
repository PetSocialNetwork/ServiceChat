using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceChat.Domain.Services;
using ServiceChat.WebApi.Models.Requests;
using ServiceChat.WebApi.Models.Responses;
using System.Runtime.CompilerServices;

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
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));         
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpDelete("[action]")]
        public async Task DeleteMessageAsync([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            await _messageService.DeleteMessageAsync(id, cancellationToken);
        }

        [HttpPost("[action]")]
        public async Task<List<MessageResponse>> BySearchAsync([FromBody] MessageRequest request, CancellationToken cancellationToken)
        {
            var messages = await _messageService.BySearchAsync(request.ChatId, request.Take, request.Offset, cancellationToken);
            return _mapper.Map<List<MessageResponse>>(messages);
        }
      
        [HttpGet("[action]")]
        public async Task<MessageResponse> GetMessageByIdAsync([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var message = await _messageService.GetMessageByIdAsync(id, cancellationToken);
            return _mapper.Map<MessageResponse>(message);
        }

        [HttpPut("[action]")]
        public async Task UpdateMessageAsync([FromBody] UpdateMessageRequest request, CancellationToken cancellationToken)
        {
            await _messageService.UpdateMessageAsync(request.Id, request.MessageText, cancellationToken);
        }

        [HttpGet("[action]")]
        public async Task<LastMessageResponse?> GetLastMessageByChatIdAsync([FromQuery] Guid chatId, CancellationToken cancellationToken)
        {
            var message = await _messageService.GetLastMessageByChatIdAsync(chatId, cancellationToken);
            if (message != null)
            {
                return _mapper.Map<LastMessageResponse>(message);
            }
            return new LastMessageResponse();
        }
    }
}
