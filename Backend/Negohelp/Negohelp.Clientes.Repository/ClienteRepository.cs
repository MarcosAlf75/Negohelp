using System.Data;
using System.Transactions;
using Negohelp.Clientes.Interface;
using Negohelp.Comun.DataAccess;
using Negohelp.Comun.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Negohelp.Clientes.Repository
{
	public class ClienteRepository : IClienteRepository
	{
		public ClienteRepository()
		{
		}

		#region Metodos para Consultar Clientes
		public ICliente ConsultarClienteId(int id)
		{
			ICliente cliente;

			IDataAccess dao = new Dao { CadenaDeConexion = "Clientes" };
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

				if (dsResultado.Tables[0].Rows.Count > 0)
				{
					if (dsResultado.Tables[0].Columns.Contains("TipoCliente"))
					{
						if (Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["TipoCliente"]))
						{
							cliente = new ClienteEmpresa();
							cliente = Mapeador.DataTableToObject<ClienteEmpresa>(dsResultado.Tables[0]);
						}
						else
						{
							cliente = new ClientePersona();
							cliente = Mapeador.DataTableToObject<ClientePersona>(dsResultado.Tables[0]);
						}
						return cliente;
					}
					return null;
				}
				else
					return null;
			}
		}

		public ICliente ConsultarClienteIdentificacion(string identificacion)
		{
			ICliente cliente;
			IDataAccess dao = new Dao { CadenaDeConexion = "Clientes" };

			List<SqlParametro> lstParametros = new List<SqlParametro>
			{
				new SqlParametro("ReturnValue", SqlDbType.Int, ParameterDirection.ReturnValue, 4),
				new SqlParametro("@Identificacion", SqlDbType.VarChar, 4, identificacion),
			};

			using(dao)
			{
				var dsResultado = dao.EjecutarSp("ConsultarClienteIdentificacion", lstParametros);
				if (dao.ParametroRetorno > 0)
					return null;

				if (dsResultado.Tables[0].Rows.Count <= 0)
					return null;

				//cliente = ValidarTipoCliente();
;
				if (dsResultado.Tables[0].Columns.Contains("TipoCliente"))
				{
					if (Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["TipoCliente"]))
					{
						cliente = new ClienteEmpresa();
						cliente = Mapeador.DataTableToObject<ClienteEmpresa>(dsResultado.Tables[0]);
					}
					else
					{
						cliente = new ClientePersona();
						cliente = Mapeador.DataTableToObject<ClientePersona>(dsResultado.Tables[0]);
					}
					return cliente;
				}
			}
			return null;
		}

		public ICollection<ICliente> ConsultarNombreEmpresa(string razonSocial)
		{
			throw new NotImplementedException();
		}

		public ICollection<ICliente> ConsultarNombrePersona(string primerApellido, string segundoApellido, string nombre)
		{
			throw new NotImplementedException();
		}

		#endregion

		public ClienteEmpresa CrearClienteEmpresa(ClienteEmpresa empresa)
		{
			throw new NotImplementedException();
		}

		public ClientePersona CrearClientePersona(ClientePersona persona)
		{
			int clienteId;
			ClientePersona cliente;

			IDataAccess dao = new Dao { CadenaDeConexion = "Clientes" };

			//identificacion, tipoId, nombre, primerApellido, segundoApellido, fechaNacimiento

			List<SqlParametro> lstParametros = new List<SqlParametro>
			{
				new SqlParametro("ReturnValue", SqlDbType.Int, ParameterDirection.ReturnValue, 4),
				new SqlParametro("@IdCliente", SqlDbType.Int, ParameterDirection.Output, 4),
				new SqlParametro("@TipoCliente", SqlDbType.Int, 4, persona.TipoCliente),
				new SqlParametro("@Identificacion", SqlDbType.VarChar, 4, persona.Identificacion),
				new SqlParametro("@TipoIdentificacion", SqlDbType.Int, 4, persona.TipoIdentificacion),
				new SqlParametro("@Nombre", SqlDbType.VarChar, 50, persona.Nombre),
				new SqlParametro("@PrimerApellido", SqlDbType.VarChar, 40, persona.PrimerApellido),
				new SqlParametro("@SegundoApellido", SqlDbType.VarChar, 40, persona.SegundoApellido),
				new SqlParametro("@FechaNacimiento", SqlDbType.DateTime, 3, persona.FechaNacimiento),
			};

			using (dao)
			{
				var dsResultado = dao.EjecutarSp("CrearClientePersona", lstParametros);
				if (dao.ParametroRetorno > 0)
					return null;

				if (dao.ParametrosSalida[0].Value == null)
					return null;

				clienteId = Convert.ToInt32(dao.ParametrosSalida[0].Value);
			};

			cliente = (ClientePersona)ConsultarClienteId(clienteId);

			return cliente;
		}

		#region Metodos Privados
		private TipoCliente DevolverTipoCliente()
		{
			return TipoCliente.ClientePersona;

		}
	}
}
