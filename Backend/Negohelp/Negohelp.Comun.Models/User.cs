﻿namespace Negohelp.Comun.Models
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public DateTime FechaCreacion { get; set; }
		public bool Vigente { get; set; }
	}
}