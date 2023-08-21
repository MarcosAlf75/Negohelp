using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace Negohelp.Comun.DataAccess
{
	public class Dao : IDataAccess
	{
		public Dictionary<string, SqlParametro> Parametros { get; set; }

		#region Contructores
		//contructores y destructores
		/// <summary>constructor necesita de la propiedad CadenaDeConexión.</summary>
		public Dao()
		{
			Parametros = new Dictionary<string, SqlParametro>();
		}
		#endregion

		#region Variables privadas
		//Variables para depuración
		private static byte nivelDebug = 0;
		private static int rowCountDefault = 6000;

		//Variables para ejecución de comandos
		private SqlConnection cnnConexion = null;
		private int parReturnParametro;
		private SqlCommand cmdComando;
		private string conexion = "";
		private string strsp = "";
		private int intTimeOut = 0;
		private int intNumOut;
		private string Isolationlevel = "ReadCommitted";

		// Variables para paginación
		private string esquema = "";
		#endregion

		#region Propiedades
		//Propiedades
		/// <summary>propiedad de lectura para recibir número de parámetros de salida.</summary>
		public int NumeroDeParametrosSalida
		{
			get { return intNumOut; }
		}

		/// <summary>Propiedad de lectura/escritura para el nombre de la cadena de conexión</summary>
		public string CadenaDeConexion
		{
			get { return conexion; }
			set
			{
				string nombreBase = value;
				string cadenaConexion = LeerConexion();
				conexion = "";

				if (cadenaConexion != null)
				{
					JsonDocument jsonDoc = JsonDocument.Parse(cadenaConexion);
					JsonElement root = jsonDoc.RootElement;
					JsonElement datoConexion = root.GetProperty("InformacionConexion").GetProperty(value);

					intTimeOut = datoConexion.GetProperty("Connect Timeout").GetInt32();

					foreach (JsonProperty property in datoConexion.EnumerateObject())
					{
						conexion = conexion == "" ? "" : conexion + ";";
						conexion = conexion + property.Name + "=" + property.Value.ToString();
					}
				}
				rowCountDefault = 3000;
				nivelDebug = 0;
			}
		}

		/// <summary>propiedad de lectura que contiene un arreglo de parámetros de salida.</summary>
		public List<SqlParametro> ParametrosSalida
		{
			get
			{
				List<SqlParametro> parOutParametro = new List<SqlParametro>();

				foreach (SqlParameter sqlParametro in cmdComando.Parameters)
				{
					if (sqlParametro.Direction == ParameterDirection.Output)
					{
						var paramSalida = new SqlParametro(sqlParametro.ParameterName, sqlParametro.SqlDbType, sqlParametro.Size);
						paramSalida.Value = sqlParametro.Value;
						paramSalida.Direction = sqlParametro.Direction;
						parOutParametro.Add(paramSalida);
					}
				}
				return parOutParametro;
			}
		}

		/// <summary>propiedad de lectura que contiene el parámetro de retorno.</summary>
		public int ParametroRetorno
		{
			get { return parReturnParametro; }
		}

		/// <summary>propiedad de lectura que contiene el tiempo de espera para ejecutar una sentencia SQL o un SP.</summary>
		public int TimeOut
		{
			get { return intTimeOut; }
		}

		/// <summary>
		/// Propiedad para determinar el nivel de aislamiento de la transacción 
		/// </summary>
		public string NivelAislamiento
		{
			set
			{
				Isolationlevel = value;
			}
		}
		#endregion

		

		#region Métodos públicos
		//metodos públicos
		/// <summary>destructor de variables privadas</summary>		
		public void LDispose()
		{
			if (cmdComando != null)
			{
				SqlConnection cnnConexion = cmdComando.Connection;
				Debug.Assert(cnnConexion != null);
				cmdComando.Dispose();
				cmdComando = null;
				cnnConexion.Dispose();
			}
		}

		/// <summary>ejecuta un proceso almacenado que puede o no retornar un DataSet.</summary>
		///<param name="sp">(Remote Procedure Call) nombre del proceso almacenado</param>
		///<returns>retorna verdadero si se ejecutó correctamente</returns>
		public DataSet EjecutarSp(string sp)
		{
			return EjecutarSp(sp, ObtenerParametros(), intTimeOut);
		}

		/// <summary>ejecuta un proceso almacenado que puede o no retornar un DataSet.</summary>
		///<param name="sp">(Remote Procedure Call) nombre del proceso almacenado</param>
		///<param name="listaParametros">Lista de parámetros necesarios para la ejecución del sp</param>
		///<returns>retorna verdadero si se ejecutó correctamente</returns>
		public DataSet EjecutarSp(string sp, List<SqlParametro> listaParametros)
		{
			return EjecutarSp(sp, listaParametros, intTimeOut);
		}

		/// <summary>Ejecuta un procedimiento almacenado que puede o no retornar Datos</summary>
		///<param name="sp">Nombre del proceso almacenado</param>
		///<param name="timeOut">parámetro que contiene el tiempo de espera para ejecutar el sp</param>
		///<returns>retorna verdadero si se ejecutó correctamente</returns>
		public DataSet EjecutarSp(string sp, int timeOut)
		{
			return EjecutarSp(sp, ObtenerParametros(), timeOut);
		}

		/// <summary>ejecuta un proceso almacenado que puede o no retornar un DataSet.</summary>
		///<param name="sp">(Remote Procedure Call) nombre del proceso almacenado</param>
		///<param name="listaParametros">Arreglo de parámetros necesarios para la ejecución del sp</param>
		///<param name="timeOut">parámetro que contiene el tiempo de espera para ejecutar el sp.</param>
		///<returns>retorna verdadero si se ejecutó correctamente</returns>
		public DataSet EjecutarSp(string sp, List<SqlParametro> listaParametros, int timeOut)
		{
			string Cadena = "";
			string CadenaTmp = "";
			DateTime HoraInicio;
			DateTime HoraFin;
			DateTime HoraInicioTmp;
			DateTime HoraFinTmp;
			int NumeroTotalRegistros = 0;
			TimeSpan hgTmp;
			DataSet dsDatos = new DataSet();
			intNumOut = 0;

			try
			{
				HoraInicio = DateTime.Now;

				strsp = sp;
				HoraInicioTmp = DateTime.Now;
				if (Conectar(sp, CommandType.StoredProcedure))
				{
					if (nivelDebug > 1)
					{
						HoraFinTmp = DateTime.Now;
						hgTmp = HoraFinTmp - HoraInicioTmp;
						CadenaTmp = "Conectar: " + hgTmp.TotalSeconds.ToString();
					}

					if (listaParametros != null)
					{
						SqlParameter[] parParametros = new SqlParameter[listaParametros.Count];
						for (int i = 0; i < listaParametros.Count; ++i)
						{
							parParametros[i] = new SqlParameter();
							parParametros[i].Direction = listaParametros[i].Direction;
							parParametros[i].ParameterName = listaParametros[i].Name;
							parParametros[i].Size = listaParametros[i].Size;
							parParametros[i].SqlDbType = listaParametros[i].Type;
							parParametros[i].Value = listaParametros[i].Value;
							if (listaParametros[i].Precision != 0)
							{
								parParametros[i].Precision = listaParametros[i].Precision;
								parParametros[i].Scale = listaParametros[i].Scale;
							}
							else if (listaParametros[i].Type == SqlDbType.Decimal)
							{
								parParametros[i].Precision = 28;
								parParametros[i].Scale = 4;
							}
						}
						foreach (SqlParameter sqlParametro in parParametros)
						{
							if (sqlParametro.Direction == ParameterDirection.Output)
							{
								intNumOut++;
							}
							else if (sqlParametro.Direction == ParameterDirection.ReturnValue)
							{
								sqlParametro.Value = -999;
							}
							cmdComando.Parameters.Add(sqlParametro);
						}
					}
					SqlDataAdapter daDataAdapter = new SqlDataAdapter();
					cmdComando.CommandTimeout = TimeOut;

					HoraInicioTmp = DateTime.Now;

					Isolation();

					if (nivelDebug > 1)
					{
						HoraFinTmp = DateTime.Now;
						hgTmp = HoraFinTmp - HoraInicioTmp;
						CadenaTmp += "\tIsolation: " + hgTmp.TotalSeconds.ToString();
					}

					HoraInicioTmp = DateTime.Now;
					AplicarSetRowCountDefault();
					if (nivelDebug > 1)
					{
						HoraFinTmp = DateTime.Now;
						hgTmp = HoraFinTmp - HoraInicioTmp;
						CadenaTmp += "\tAplicarSetRowCountDefault: " + hgTmp.TotalSeconds.ToString();
					}

					daDataAdapter.SelectCommand = cmdComando;

					HoraInicioTmp = DateTime.Now;
					daDataAdapter.Fill(dsDatos, "tabla");

					if (nivelDebug > 1)
					{
						HoraFinTmp = DateTime.Now;
						hgTmp = HoraFinTmp - HoraInicioTmp;
						CadenaTmp += "\tFill: " + hgTmp.TotalSeconds.ToString();
					}

					foreach (SqlParameter sqlParametro in cmdComando.Parameters)
					{
						if (sqlParametro.Direction == ParameterDirection.ReturnValue)
						{
							parReturnParametro = int.Parse(sqlParametro.Value.ToString());
						}
					}

					if (nivelDebug > 0)
					{
						HoraFin = DateTime.Now;
						TimeSpan hg = HoraFin - HoraInicio;
						string Milisegundos = (HoraInicio.Millisecond / 1000.0).ToString().Substring(1);
						Cadena += HoraInicio.ToString("G") + Milisegundos + "\t"
							+ hg.TotalSeconds.ToString() + "\t";
						if (dsDatos != null)
						{
							Cadena += dsDatos.Tables.Count + "\t";
							NumeroTotalRegistros = 0;
							for (int i = 0; i <= (dsDatos.Tables.Count - 1); i++)
								NumeroTotalRegistros += dsDatos.Tables[i].Rows.Count;
							Cadena += NumeroTotalRegistros + "\t";
						}
						Cadena += sp + "\t"
							+ CadenaTmp + "\t";
					}
					if (nivelDebug > 2)
					{
						if (listaParametros != null)
						{
							for (int i = 0; i < listaParametros.Count; ++i)
							{
								Cadena += listaParametros[i].Name + "=" + listaParametros[i].Value + " ";
							}
						}
					}
					if (nivelDebug > 0)
						EscribeArchivo(Cadena);
				}
			}
			catch
			{
				dsDatos = null;
				throw;
			}
			finally
			{
				Desconectar();
			}

			return dsDatos;
		}
		#endregion

		#region Métodos privados
		private List<SqlParametro> ObtenerParametros()
		{
			List<SqlParametro> listaParametros = null;

			if (Parametros.Count > 0)
				listaParametros = Parametros.Values.ToList();

			return listaParametros;
		}

		private void Isolation()
		{
			SqlCommand cmd = new SqlCommand();
			switch (Isolationlevel)
			{
				case "ReadCommitted":
					cmd.CommandText = "set transaction isolation level read committed";
					break;
				case "ReadUncommitted":
					cmd.CommandText = "set transaction isolation level read uncommitted";
					break;
				case "RepeatableRead":
					cmd.CommandText = "set transaction isolation level repeatable read";
					break;
				case "Serializable":
					cmd.CommandText = "set transaction isolation level serializable";
					break;
				default:
					cmd.CommandText = "set transaction isolation level read committed";
					break;
			}
			cmd.Connection = cnnConexion;
			cmd.Connection.Open();
			cmd.ExecuteNonQuery();
			cmd = null;
		}

		private void AplicarSetRowCountDefault()
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "set rowcount " + rowCountDefault.ToString();
			cmd.Connection = cnnConexion;
			cmd.ExecuteNonQuery();
			cmd = null;
		}

		private bool Conectar(string sentencia, CommandType tipo)
		{
			if (string.IsNullOrEmpty(conexion))
				throw new Exception("Se necesita una cadena de conexión");

			cnnConexion = new SqlConnection(conexion);
			cmdComando = new SqlCommand(esquema + sentencia, cnnConexion);
			cmdComando.CommandType = tipo;
			return true;
		}

		private void Desconectar()
		{
			if (cnnConexion != null)
			{
				cnnConexion.Dispose();
				cnnConexion = null;
			}
		}

		private void EscribeArchivo(string Cadena)
		{
			Stream _archoutput = null;
			StreamWriter output = null;
			try
			{
				string NombreArchivo;
				NombreArchivo = "Negohelp.Dao_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
				_archoutput = new FileStream(NombreArchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite);
				output = new StreamWriter(_archoutput);
				output.BaseStream.Seek(0, SeekOrigin.End);
				output.WriteLine(Cadena);
			}
			catch
			{ }
			finally
			{
				if (output != null)
					output.Close();
				if (_archoutput != null)
					_archoutput.Close();
			}
		}
		private string LeerConexion()
		{
			// Replace "YourNamespace" with the actual namespace of your class library project.
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "Negohelp.Comun.DataAccess.negohelp.json";

			// Read the embedded resource stream (JSON file content).
			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				if (stream != null)
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						// Read the content of the JSON file.
						string jsonContent = reader.ReadToEnd();
						return jsonContent;
					}
				}
				else
				{
					return "";
				}
			}
		}
		#endregion

		#region IDisposable Members
		/// <summary>
		/// Dispose de variables internas
		/// </summary>
		public void Dispose()
		{
			this.LDispose();
		}

		#endregion
	}
}
