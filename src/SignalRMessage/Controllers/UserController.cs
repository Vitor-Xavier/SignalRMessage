using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRMessage.Crosscutting;
using SignalRMessage.Services.User;
using System.Threading;

namespace SignalRMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        /// <summary>
        /// Authenticate user based on credentials.
        /// </summary>
        /// <param name="authenticateModel">User credentials</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>User</returns>
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public Models.User Authenticate(AuthenticateModel authenticateModel, CancellationToken cancellationToken) =>
            _userService.Authenticate(authenticateModel.Username, authenticateModel.Password, cancellationToken);
    }
}
