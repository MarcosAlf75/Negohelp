using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negohelp.ValidarDatos
{
	public class ValidationResult<T>
	{
		public bool ValidationSuccess { get; set; }
		public string ValidationMessage { get; set; } = string.Empty;
		public T? ValidationData { get; set; }
	}
}
