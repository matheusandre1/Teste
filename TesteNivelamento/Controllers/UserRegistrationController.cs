using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteNivelamento.Interfaces;
using TesteNivelamento.Models;

namespace TesteNivelamento.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserRegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]

        public IActionResult Register([FromBody] User user)
        {
            var creationUser = _userService.Create(user, user.Password);

            return Ok(creationUser);
        }

        [Authorize]
        [HttpPut]
        [Route("/api/users/{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            var updateUser = _userService.GetById(id);

            if (updateUser == null)
            {
                return NotFound();
            }

            if (updateUser.Id != int.Parse(User.Identity.Name))
            { 
                return Unauthorized();
            }

            _userService.Update(user, user.Password);
            return Ok();


        }
    }
}