using Negohelp.Comun.DataAccess;
using Negohelp.Comun.Models;
using Negohelp.IRepository;
using System.Data;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Negohelp.Repository
{
	public class ClienteRepository : IClienteRepository
	{
		public ClienteRepository()
		{
		}

		#region Metodos para Consultar Clientes

		public ICliente ConsultarClienteIdentificacion(string identificacion)
		{
			ICliente cliente;
			IDataAccess dao = new Dao { CadenaDeConexion = "Negohelp" };
			DataRow drDatos;

			List<SqlParametro> lstParametros = new List<SqlParametro>
			{
				new SqlParametro("ReturnValue", SqlDbType.Int, ParameterDirection.ReturnValue, 4),
				new SqlParametro("@Identificacion", SqlDbType.VarChar, 20, identificacion),
			};

			using (dao)
			{
				var dsResultado = dao.EjecutarSp("ConsultarClienteIdentificacion", lstParametros);
				if (dao.ParametroRetorno > 0)
					return null;

				if (dsResultado.Tables[0].Rows.Count <= 0)
					return null;

				drDatos = dsResultado.Tables[0].Rows[0];
				;
				if (dsResultado.Tables[0].Columns.Contains("TipoCliente"))
				{
					if (Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TipoCliente"]) == 2)
					{
						cliente = new ClienteEmpresa();
						cliente = Mapeador.DataRowToObject<ClienteEmpresa>(drDatos);
					}
					else if (Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TipoCliente"]) == 1)
					{
						cliente = new ClientePersona();
						cliente = Mapeador.DataRowToObject<ClientePersona>(drDatos);
					}
					else
						return null;

					return cliente;
				}
			}
			return null;
		}

		public List<ListaClientes> ConsultarClienteNombre(string nombreCliente)
		{
			List<ListaClientes> clientes; // = new List<ClientesDto>();

			IDataAccess dao = new Dao { CadenaDeConexion = "Negohelp" };

			List<SqlParametro> lstParametros = new List<SqlParametro>
			{
				new SqlParametro("ReturnValue", SqlDbType.Int, ParameterDirection.ReturnValue, 4),
				new SqlParametro("@nombre", SqlDbType.VarChar, 50, nombreCliente),
			};

			using (dao)
			{
				var dsResultado = dao.EjecutarSp("ConsultarClienteNombre", lstParametros);
				if (dao.ParametroRetorno > 0)
					return null;

				if (dsResultado.Tables[0].Rows.Count <= 0)
					return null;

				clientes = Mapeador.DataTableToList<ListaClientes>(dsResultado.Tables[0]);

				return clientes;
			}
		}

		#endregion

		#region Metodos para creacion de clientes
		public ClienteEmpresa CrearClienteEmpresa(ClienteEmpresa empresa)
		{
			ClienteEmpresa cliente;

			List<SqlParametro> lstParametros = new List<SqlParametro>
			{
				new SqlParametro("ReturnValue", SqlDbType.Int, ParameterDirection.ReturnValue, 4),
				new SqlParametro("@IdCliente", SqlDbType.Int, ParameterDirection.Output, 4),
				new SqlParametro("@TipoCliente", SqlDbType.Int, 4, empresa.TipoCliente),
				new SqlParametro("@Identificacion", SqlDbType.VarChar, 20, empresa.Identificacion),
				new SqlParametro("@TipoIdentificacion", SqlDbType.Int, 4, empresa.TipoIdentificacion),
				new SqlParametro("@Nombre", SqlDbType.VarChar, 50, empresa.Nombre)
			};

			cliente = (ClienteEmpresa)EjecutarSP("CrearClienteEmpresa", lstParametros);

			return cliente;
		}

		public ClientePersona CrearClientePersona(ClientePersona persona)
		{
			ClientePersona cliente;
			IDataAccess dao = new Dao { CadenaDeConexion = "Negohelp" };

			List<SqlParametro> lstParametros = new List<SqlParametro>
			{
				new SqlParametro("ReturnValue", SqlDbType.Int, ParameterDirection.ReturnValue, 4),
				new SqlParametro("@IdCliente", SqlDbType.Int, ParameterDirection.Output, 4),
				new SqlParametro("@TipoCliente", SqlDbType.Int, 4, persona.TipoCliente),
				new SqlParametro("@Identificacion", SqlDbType.VarChar, 20, persona.Identificacion),
				new SqlParametro("@TipoIdentificacion", SqlDbType.Int, 4, persona.TipoIdentificacion),
				new SqlParametro("@Nombre", SqlDbType.VarChar, 50, persona.Nombre),
				new SqlParametro("@PrimerApellido", SqlDbType.VarChar, 40, persona.PrimerApellido),
				new SqlParametro("@SegundoApellido", SqlDbType.VarChar, 40, persona.SegundoApellido),
				new SqlParametro("@FechaNacimiento", SqlDbType.DateTime, 3, persona.FechaNacimiento),
			};

			cliente = (ClientePersona)EjecutarSP("CrearClientePersona", lstParametros);

			return cliente;

		}
		#endregion

		#region Metodos Privados

		internal ICliente ConsultarClienteId(int id)
		{
			ICliente cliente;
			DataRow drDatos;

			IDataAccess dao = new Dao { CadenaDeConexion = "Negohelp" };
			List<SqlParametro> lstParametros = new List<SqlParametro>
			{
				new SqlParametro("ReturnValue", SqlDbType.Int, ParameterDirection.ReturnValue, 4),
				new SqlParametro("@Id", SqlDbType.Int, 4, id),
			};

			using (dao)
			{
				var dsResultado = dao.EjecutarSp("ConsultarClienteId", lstParametros);

				if (dao.ParametroRetorno > 0)
					return null;

				if (dsResultado.Tables[0].Rows.Count == 0)
					return null;

				drDatos = dsResultado.Tables[0].Rows[0];

				if (dsResultado.Tables[0].Columns.Contains("TipoCliente"))
				{
					if (Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TipoCliente"]) == 2)
					{
						cliente = new ClienteEmpresa();
						cliente = Mapeador.DataRowToObject<ClienteEmpresa>(drDatos);
					}
					else if (Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TipoCliente"]) == 1)
					{
						cliente = new ClientePersona();
						cliente = Mapeador.DataRowToObject<ClientePersona>(drDatos);
					}
					else
						return null;

					return cliente;
				}
				return null;
			}
		}

		private ICliente EjecutarSP(string nombreSp, List<SqlParametro> lstParametros)
		{
			int clienteId;

			ICliente cliente;
			IDataAccess dao = new Dao { CadenaDeConexion = "Negohelp" };

			using (dao)
			{
				var dsResultado = dao.EjecutarSp(nombreSp, lstParametros);
				if (dao.ParametroRetorno > 0)
					return null;

				if (dao.ParametrosSalida[0].Value == null)
					return null;

				clienteId = Convert.ToInt32(dao.ParametrosSalida[0].Value);
			};

			cliente = ConsultarClienteId(clienteId);

			return cliente;
		}
		#endregion
	}
}
