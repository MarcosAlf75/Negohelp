using System.ComponentModel.DataAnnotations;

namespace Negohelp.Comun.Models
{
	public class UserDto
	{
		[Required]
		public required string UserName { get; set; }
		[Required]
		public required string Password { get; set; }
	}

}
