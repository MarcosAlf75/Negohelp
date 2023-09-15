using Negohelp.Comun.Models;
using Microsoft.AspNetCore.Mvc;

namespace Negohelp.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AutenticacionController : ControllerBase
	{
		public static User user = new User();

		[HttpPost("register")]
		public ActionResult<User> Registrar(UserDto userRequest)
		{
			string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);

			user.UserName = userRequest.UserName;
			user.PasswordHash = passwordHash;
			
			return Ok(user);

		}

		[HttpPost("login")]
		public ActionResult<User> Login(UserDto userLogin)
		{
			if (user.UserName != userLogin.UserName)
				return BadRequest("Bad");

			if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, user.PasswordHash))
				return BadRequest("Bad P");

			return Ok();
		}
	}
}
