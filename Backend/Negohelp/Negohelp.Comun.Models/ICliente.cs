using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negohelp.Comun.Models
{
	public interface ICliente
	{
		int Id { get; set; }
		TipoCliente TipoCliente{ get; set; }
		string Identificacion { get; set; }
		TipoIdentificacion TipoIdentificacion { get; set; }
		string Nombre { get; set; }
		DateTime LogFechaCreacion { get; set; }
		DateTime LogFechaActualizacion { get; set; }
		DateTime LogFechaEliminacion { get; set; }
		bool Vigente { get; set; }
	}
}
