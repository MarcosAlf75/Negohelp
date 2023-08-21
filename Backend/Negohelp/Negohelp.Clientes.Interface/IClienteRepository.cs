using System;
using Negohelp.Comun.Models;

namespace Negohelp.Clientes.Interface
{
	public interface IClienteRepository
	{
		/// <summary>
		/// Busqueda de un cliente con Id interno de la base
		/// </summary>
		/// <param name="id">Id interno del cliente</param>
		/// <returns>Objeto ClientePersona o ClienteEmpresa</returns>
		ICliente ConsultarClienteId(int id);
		/// <summary>
		/// Busqueda de un cliente con identificacion
		/// </summary>
		/// <param name="identificacion">Identificacion del cliente, Cedula, RUC, Pasaporte</param>
		/// <returns></returns>
		ICliente ConsultarClienteIdentificacion(string identificacion);
		/// <summary>
		/// Busqueda cliente por su primer apellido
		/// </summary>
		/// <param name="primerApellido">primer apellido de la persona</param>
		/// <returns>Clientes que coicidan con el apellido</returns>
		ICollection<ICliente> ConsultarNombrePersona(string primerApellido, string segundoApellido, string nombre);
		/// <summary>
		/// Busqueda de empresa por razon social o nombre comercial
		/// </summary>
		/// <param name="nombreEmpresa">Nombre de la empresa</param>
		/// <param name="tipoNombre">RS razon social -- NC nombre comercial</param>
		/// <returns>Retorna lista de clientes que coinciden con el nombre</returns>
		ICollection<ICliente> ConsultarNombreEmpresa(string razonSocial);
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
