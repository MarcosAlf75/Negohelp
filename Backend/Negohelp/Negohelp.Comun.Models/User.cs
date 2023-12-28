using System.ComponentModel.DataAnnotations;

namespace Negohelp.Comun.Models
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public DateTime FechaCreacion { get; set; }
		public bool Vigente { get; set; }
	}

	public class UserRegister
	{
		[Required]
		[UserNameFormat(ErrorMessage = "Nombre de usuario en formato incorrecto")]
		public required string UserName { get; set; }

		[Required]
		[PasswordFormat(ErrorMessage = "Password no cumple el formato establecido")]
		public required string Password { get; set; }
		[Required]
		[EmailAddress(ErrorMessage = "Direccion email invalida")]
		public string? Email { get; set; }
	}
}
