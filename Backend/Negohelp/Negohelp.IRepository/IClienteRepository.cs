using Negohelp.Comun.Models;

namespace Negohelp.IRepository
{
	public interface IClienteRepository
	{
		/// <summary>
		/// Busqueda de un cliente con Id interno de la base
		/// </summary>
		/// <param name="id">Id interno del cliente</param>
		/// <returns>Objeto ClientePersona o ClienteEmpresa</returns>
		//ICliente ConsultarClienteId(int id);
		/// <summary>
		/// Busqueda de un cliente con identificacion
		/// </summary>
		/// <param name="identificacion">Identificacion del cliente, Cedula, RUC, Pasaporte</param>
		/// <returns></returns>
		ICliente ConsultarClienteIdentificacion(string identificacion);
		/// <summary>
		/// Busqueda de clientes por su nombre o apellidos, personas o empresas
		/// </summary>
		/// <param name="nombre o apellido a ser buscado">primer apellido de la persona</param>
		/// <returns>Clientes que coicidan con el apellido</returns>
		List<ListaClientes> ConsultarClienteNombre(string nombreCliente);
		/// <summary>
		/// Crear cliente Persona
		/// </summary>
		/// <param name="persona">Objecto persona con datos para la creacion</param> 
		/// <returns></returns>
		ClientePersona CrearClientePersona(ClientePersona persona);
		/// <summary>
		/// Crear cliente Empresa
		/// </summary>
		/// <param name="empresa">Objecto empresa con datos para la creacion</param> 
		/// <returns></returns>
		ClienteEmpresa CrearClienteEmpresa(ClienteEmpresa empresa);
	}
}
