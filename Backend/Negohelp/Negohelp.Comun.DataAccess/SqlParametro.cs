using System.Data;
using System.Data.SqlClient;

namespace Negohelp.Comun.DataAccess
{
	/// <summary>
	/// Parametros que recibe un procedimiento almacenado
	/// </summary>
	[Serializable]
	public class SqlParametro
	{
		/// <summary>
		/// Nombre del Parametro
		/// </summary>
		public string Name;
		/// <summary>
		/// Tipo SqlDbType para el parametro
		/// </summary>
		public SqlDbType Type;
		/// <summary>
		/// Tamaño en bytes del valor del parametro
		/// </summary>
		public int Size;
		/// <summary>
		/// Valor del parametro
		/// </summary>
		public Object Value;
		/// <summary>
		/// Direction del parametro Entrada/Salida/Retorno
		/// </summary>
		public ParameterDirection Direction;
		/// <summary>
		/// Precision del valor del parametro
		/// </summary>
		public byte Precision;
		/// <summary>
		/// Escala del valor del parametro
		/// </summary>
		public byte Scale;
		/// <summary>
		/// Crea un nuevo parametro
		/// </summary>
		/// <param name="nombreParametro">Nombre del parametro</param>
		/// <param name="tipoDato">Tipo SqlDbType para el parametro</param>
		/// <param name="tamanio">Tamaño en bytes del valor</param>
		public SqlParametro(string nombreParametro, System.Data.SqlDbType tipoDato, int tamanio)
		{
			Name = nombreParametro;
			Type = tipoDato;
			Size = tamanio;
			Value = null;
			Direction = ParameterDirection.Input;
			Precision = 0;
			Scale = 0;
		}
		/// <summary>
		/// Crea un nuevo parámetro
		/// </summary>
		/// <param name="nombreParametro">Nombre del Parámetro</param>
		/// <param name="tipoDato">Tipo SqlDbType para el parametro</param>
		/// <param name="tamanio">Tamaño en bytes del valor</param>
		/// <param name="valor">Valor del Parametro</param>
		public SqlParametro(string nombreParametro, System.Data.SqlDbType tipoDato, int tamanio, Object valor)
		{
			Name = nombreParametro;
			Type = tipoDato;
			Size = tamanio;
			Value = valor;
			Direction = ParameterDirection.Input;
			Precision = 0;
			Scale = 0;
		}

		/// <summary>
		/// Crea un nuevo parámetro
		/// </summary>
		/// <param name="nombreParametro">Nombre del Parámetro</param>
		/// <param name="valor">Valor del Parametro</param>
		public SqlParametro(string nombreParametro, Object valor)
		{
			var sqlParameter = new SqlParameter(nombreParametro, valor);

			Name = nombreParametro;
			Type = sqlParameter.SqlDbType;
			Size = 0;
			Value = valor;
			Direction = ParameterDirection.Input;
			Precision = 0;
			Scale = 0;
		}

		/// <summary>
		/// Crea un nuevo parámetro
		/// </summary>
		/// <param name="nombreParametro">Nombre del Parámetro</param>
		/// <param name="tipoDato">Valor del Parametro</param>
		/// <param name="tamanio">Tamanio del parámetro y es opcional; puede enviarse un valor nulo</param>
		/// <param name="direccion">Dirección del parámetro</param>
		public SqlParametro(string nombreParametro, SqlDbType tipoDato, ParameterDirection direccion, int? tamanio)
		{
			SqlParameter sqlParameter = null;
			if (tamanio.HasValue)
				sqlParameter = new SqlParameter(nombreParametro, tipoDato, tamanio.Value);
			else
				sqlParameter = new SqlParameter(nombreParametro, tipoDato);

			Name = nombreParametro;
			Type = sqlParameter.SqlDbType;
			Size = sqlParameter.Size;
			Direction = direccion;
			Precision = 0;
			Scale = 0;
			Value = null;
		}

		/// <summary>
		/// Crea parámetro para el valor de retorno
		/// </summary>
		public static SqlParametro Retorno
		{
			get
			{
				var parametroRetorno = new SqlParametro("ValorRetorno", SqlDbType.Int, 4);
				parametroRetorno.Direction = ParameterDirection.ReturnValue;

				return parametroRetorno;
			}
		}
	}
}
