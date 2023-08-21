using System;
using System.Data;

namespace Negohelp.Comun.DataAccess
{
	public interface IDataAccess : IDisposable
	{
		string CadenaDeConexion { get; set; }

		string NivelAislamiento { set; }

		int NumeroDeParametrosSalida { get; }

		List<SqlParametro> ParametrosSalida { get; }

		Dictionary<string, SqlParametro> Parametros { get; set; }

		int ParametroRetorno { get; }

		int TimeOut { get; }

		DataSet EjecutarSp(string sp);

		DataSet EjecutarSp(string sp, List<SqlParametro> listaParametros);

		DataSet EjecutarSp(string sp, List<SqlParametro> listaParametros, int timeOut);

		DataSet EjecutarSp(string sp, int timeOut);
	}
}
