using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace SignalRMessage.Providers
{
    public class UsernameBasedUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection) =>
            connection.User?.FindFirst(ClaimTypes.Name)?.Value;
    }
}
