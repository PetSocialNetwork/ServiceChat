﻿using AutoMapper;
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

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageFoundException))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("[action]")]
        public async Task DeleteMessageAsync([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            await _messageService.DeleteMessageAsync(id, cancellationToken);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("[action]")]
        public async IAsyncEnumerable<MessageResponse> BySearchAsync([FromQuery] Guid chatId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (var message in _messageService.BySearchAsync(chatId, cancellationToken))
            {
                var messageResponse = _mapper.Map<MessageResponse>(message);
                yield return messageResponse;
            }
        }
      
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageFoundException))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("[action]")]
        public async Task<MessageResponse> GetMessageByIdAsync([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var message = await _messageService.GetMessageByIdAsync(id, cancellationToken);
            return _mapper.Map<MessageResponse>(message);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageFoundException))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("[action]")]
        public async Task UpdateMessageAsync([FromBody] UpdateMessageRequest request, CancellationToken cancellationToken)
        {
            await _messageService.UpdateMessageAsync(request.Id, request.MessageText, cancellationToken);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageFoundException))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
