using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRMessage.Crosscutting;
using SignalRMessage.Hubs;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageController(IHubContext<MessageHub> hubContext) => _hubContext = hubContext;

        [HttpPost("Send")]
        public async Task<IActionResult> SendMessage(Message message, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All?.SendAsync(message.Event, message.To, message.Content, cancellationToken: cancellationToken);
            return NoContent();
        }

        [HttpPost("Private/Send")]
        public async Task<IActionResult> SendPrivateMessage(Message message, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.Users(message.To, message.From)?.SendAsync(message.Event, message.From, message.Content, cancellationToken: cancellationToken);
            return NoContent();
        }
    }
}
