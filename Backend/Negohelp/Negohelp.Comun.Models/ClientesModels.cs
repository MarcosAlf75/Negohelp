namespace Negohelp.Comun.Models
{
	public interface ICliente
	{
		int Id { get; set; }
		TipoCliente TipoCliente { get; set; }
		string Identificacion { get; set; }
		TipoIdentificacion TipoIdentificacion { get; set; }
		string Nombre { get; set; }
		DateTime LogFechaCreacion { get; set; }
		DateTime LogFechaActualizacion { get; set; }
		DateTime LogFechaEliminacion { get; set; }
		bool Vigente { get; set; }
	}
	public class ClientePersona : ICliente
	{
		public int Id { get; set; }
		public TipoCliente TipoCliente { get; set; }
		public string Identificacion { get; set; } = string.Empty;
		public TipoIdentificacion TipoIdentificacion { get; set; }
		public string Nombre { get; set; } = string.Empty;
		public string PrimerApellido { get; set; } = string.Empty;
		public string SegundoApellido { get; set; } = string.Empty;
		public DateTime FechaNacimiento { get; set; }
		public DateTime LogFechaCreacion { get; set; }
		public DateTime LogFechaActualizacion { get; set; }
		public DateTime LogFechaEliminacion { get; set; }
		public bool Vigente { get; set; } = true;
	}
	public class ClienteEmpresa : ICliente
	{
		public int Id { get; set; }
		public TipoCliente TipoCliente { get; set; }
		public string Identificacion { get; set; } = string.Empty;
		public TipoIdentificacion TipoIdentificacion { get; set; }
		public string Nombre { get; set; } = string.Empty;
		public DateTime LogFechaCreacion { get; set; }
		public DateTime LogFechaActualizacion { get; set; }
		public DateTime LogFechaEliminacion { get; set; }
		public bool Vigente { get; set; }
	}
	public class ListaClientes
	{
		public TipoCliente TipoCliente { get; set; }
		public string Identificacion { get; set; } = string.Empty;
		public TipoIdentificacion TipoIdentificacion { get; set; }
		public string Nombre { get; set; } = string.Empty;
	}

}
