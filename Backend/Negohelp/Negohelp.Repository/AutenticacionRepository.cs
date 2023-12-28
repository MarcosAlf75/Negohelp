using Negohelp.Comun.DataAccess;
using Negohelp.Comun.Models;
using Negohelp.IRepository;
using System.Data;

namespace Negohelp.Repository
{
	public class AutenticacionRepository : IAutenticacionRepository
	{
		public bool Register(UserRegister userInput)
		{
			bool registerOk = false;
			//string uniqueSalt = BCrypt.Net.BCrypt.GenerateSalt();
			string passwordHash = BCrypt.Net.BCrypt.HashPassword(userInput.Password);

			IDataAccess dao = new Dao { CadenaDeConexion = "Autenticacion" };

			List<SqlParametro> lstParametros = new List<SqlParametro>
			{
				new SqlParametro("ReturnValue", SqlDbType.Int, ParameterDirection.ReturnValue, 4),
				new SqlParametro("@UserName", SqlDbType.VarChar, 15, userInput.UserName),
				new SqlParametro("@Password", SqlDbType.VarChar, 100, passwordHash),
				new SqlParametro("@Email", SqlDbType.VarChar, 50, userInput.Email)
			};

			using (dao)
			{
				var dsResultado = dao.EjecutarSp("RegistrarUsuario", lstParametros);

				if (dao.ParametroRetorno > 0)
					return registerOk;

				registerOk = true;
			}

			return registerOk;
		}

		public bool Login(UserDto userLogin)
		{
			IDataAccess dao = new Dao { CadenaDeConexion = "Autenticacion" };

			List<SqlParametro> lstParametros = new List<SqlParametro>
			{
				new SqlParametro("ReturnValue", SqlDbType.Int, ParameterDirection.ReturnValue, 4),
				new SqlParametro("@UserName", SqlDbType.VarChar, 15, userLogin.UserName)
			};

			using (dao)
			{
				var dsResultado = dao.EjecutarSp("RetonaUsuarioLogin", lstParametros);

				if (dao.ParametroRetorno > 0)
					return false;

				if (dsResultado.Tables[0].Rows.Count <= 0)
					return false;

				if (dsResultado.Tables[0].Rows[0]["passwordH"] is string passwordHash && passwordHash.Length > 0)
				{
					if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, passwordHash))
						return false;

					userLogin.Password = passwordHash;
				}
				return true;
			}
		}
	}
}
