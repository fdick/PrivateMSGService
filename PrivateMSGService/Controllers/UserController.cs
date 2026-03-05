using Microsoft.AspNetCore.Mvc;
using PrivateMSGService.API.Contracts;
using PrivateMSGService.Core.Abstractions;

namespace PrivateMSGService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UserController(IUsersService usersService)
        {
            this._usersService = usersService;
        }
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetAll()
        {
            var msges = await _usersService.GetAllUsersAsync();

            var results = msges.Select(x => new UserResponse(x.Item1.ID, x.Item1.Nickname, x.Item1.Name, x.Item1.LastName, x.Item1.Email));

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] UserRequest request)
        {
            var (user, error) = Core.Models.User.Create(
                Guid.NewGuid(),
                request.nickname,
                request.name,
                request.lastName,
                request.email
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var guid = await _usersService.CreateUserAsync(user);

            return Ok(guid);
        }
        
        [HttpDelete]
        public async Task<ActionResult<Guid>> DeleteUser(Guid userID)
        {
            var guid = await _usersService.DeleteUserAsync(userID);

            return Ok(guid);
        }

        [HttpPut]
        public async Task<ActionResult<Guid>> UpdateUser(Guid userID, [FromBody] UserRequest putData)
        {
            var guid = await _usersService.UpdateUserAsync(
                userID,
                putData.name,
                putData.lastName,
                putData.email,
                putData.nickname
                );

            return Ok(guid);
        }
    }
}
