using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetSocialNetwork.ServiceUser;
using ServiceChat.Domain.Entities;
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
        private readonly IUserProfileClient _userProfileClient;
        private readonly IMapper _mapper;
        public MessageController(MessageService messageService,
            IUserProfileClient userProfileClient,
            IMapper mapper)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _userProfileClient = userProfileClient ?? throw new ArgumentNullException(nameof(userProfileClient));
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
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("[action]")]
        public async Task<MessageResponse> AddMessageAsync([FromBody] AddMessageRequest request, CancellationToken cancellationToken)
        {         
            var message = _mapper.Map<Message>(request);
            var user = await _userProfileClient.GetUserProfileByIdAsync(message.UserId, cancellationToken);
            message.UserName = $"{user.FirstName} {user.FirstName}";
            await _messageService.AddMessageAsync(message, cancellationToken);
            return _mapper.Map<MessageResponse>(message);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageFoundException))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("[action]")]
        public async Task UpdatetUserProfileAsync([FromBody] UpdateMessageRequest request, CancellationToken cancellationToken)
        {
            await _messageService.UpdateMessageAsync(request.Id, request.MessageText, cancellationToken);
        }
    }
}
