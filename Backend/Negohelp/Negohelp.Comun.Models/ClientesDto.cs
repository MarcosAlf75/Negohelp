namespace Negohelp.Comun.Models
{
	public class ClientePersonaDto
	{
		public TipoCliente TipoCliente { get; set; }
		public string Identificacion { get; set; } = string.Empty;
		public TipoIdentificacion TipoIdentificacion { get; set; }
		public string Nombre { get; set; } = string.Empty;
		public string PrimerApellido { get; set; } = string.Empty;
		public string SegundoApellido { get; set; } = string.Empty;
		public DateTime FechaNacimiento { get; set; }
	}

	public class ClienteEmpresaDto
	{
		public TipoCliente TipoCliente { get; set; }
		public string Identificacion { get; set; } = string.Empty;
		public TipoIdentificacion TipoIdentificacion { get; set; }
		public string Nombre { get; set; } = string.Empty;
	}
}
