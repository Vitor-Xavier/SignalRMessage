using System.Threading;

namespace SignalRMessage.Services.User
{
    public interface IUserService
    {
        Models.User Authenticate(string username, string password, CancellationToken cancellationToken = default);
    }
}
