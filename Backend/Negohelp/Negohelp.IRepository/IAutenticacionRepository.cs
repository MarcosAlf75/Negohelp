using Negohelp.Comun.Models;

namespace Negohelp.IRepository
{
	public interface IAutenticacionRepository
	{
		bool Register(UserRegister user);
		bool Login(UserDto user);
		//bool Block(UserDto user);
	}
}
