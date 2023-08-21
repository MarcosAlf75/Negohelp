using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negohelp.Comun.Models
{
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
}
