using Exercise.BizLogic.Users;
using Exercise.BizLogic.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Exercise.API.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserBizLogic _userBizLogic;
        public UserController(ILogger<UserController> logger, IUserBizLogic userBizLogic)
        {
            _logger = logger;
            _userBizLogic = userBizLogic;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
        {
            var id = await _userBizLogic.CreateUser(model);

            return CreatedAtAction(nameof(CreateUser), new { id }, id);
        }
    }
}
