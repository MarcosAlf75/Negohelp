using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Negohelp.Comun.ApiHelpers;
using Negohelp.Comun.Models;
using Negohelp.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Negohelp.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AutenticacionController : ControllerBase
	{
		private readonly IAutenticacionRepository _authenticationRepository;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public AutenticacionController(IAutenticacionRepository autenticacionRepository, IMapper mapper, IConfiguration configuration)
		{
			_authenticationRepository = autenticacionRepository;
			_mapper = mapper;
			_configuration = configuration;
		}


		[HttpPost("register")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult Register(UserRegister userRequest)
		{

			var userRespond = _authenticationRepository.Register(userRequest);

			if (!ModelState.IsValid)
				return BadRequest("Error en los datos ingresados");

			return Ok(new ApiResponse<UserDto> { Success = true, Message = "Usuario creado exitosamente" });

		}

		[HttpPost("login")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult Login(UserDto userLogin)
		{
			string strToken = string.Empty;

			bool loginOk = _authenticationRepository.Login(userLogin);

			if (!ModelState.IsValid)
				return BadRequest(new ApiResponse<string> { Success = false, Message = "Formato invalido" });

			if (!loginOk)
				return Ok(new ApiResponse<UserDto> { Success = false, Message = "Usuario o Password invalidos" });

			strToken = GetToken(userLogin);

			if (String.IsNullOrEmpty(strToken))
				return Ok(new ApiResponse<UserDto> { Success = false, Message = "No se pudo generar el token intentelo nuevamente" });

			return Ok(new ApiResponse<String> { Success = true, Message = "Login satisfactorio", Data = strToken });
		}

		private string GetToken(UserDto user)
		{
			string? strToken = String.IsNullOrEmpty(_configuration.GetSection("AppSettings:Token").Value) ?
				null : _configuration.GetSection("AppSettings:Token").Value;

			if (String.IsNullOrEmpty(strToken))
				return string.Empty;


			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(strToken.ToString()));

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var token = new JwtSecurityToken(
				claims: new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName)
			},
				expires: DateTime.Now.AddDays(1),
				signingCredentials: credentials
				);

			var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

			return jwtToken;
		}
	}
}
