using Microsoft.AspNetCore.Mvc;
using Negohelp.Clientes.Interface;
using Negohelp.Comun.Models;
using Negohelp.ValidarDatos;
using System.ComponentModel.DataAnnotations;

namespace Negohelp.Clientes.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ClienteController : Controller
	{
		private readonly IClienteRepository _clienteRepository;

		public ClienteController(IClienteRepository clienteRepository)
		{
			_clienteRepository = clienteRepository;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(ICliente))]
		public IActionResult ConsultarClienteId([Required]int Id)
		{
			var cliente = _clienteRepository.ConsultarClienteId(Id);
			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			if (cliente == null)
				return NotFound(new ApiResponse<ICliente> { Success = false, Message = "Cliente no encontrado" });

			if (cliente.TipoCliente == TipoCliente.ClientePersona)
			{
				return Ok(new ApiResponse<ClientePersona> { Success = true, Data = (ClientePersona)cliente });
			}
			else
				return Ok(new ApiResponse<ClienteEmpresa> { Success = true, Data = (ClienteEmpresa)cliente });
		}

		/// <summary>
		/// Metodo para traer el cliente por su identificacion
		/// </summary>
		/// <param name="identificacion">Cedula, RUC o pasaporte</param>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(ICliente))]
		public IActionResult ConsultarClienteIdentificacion([Required]string identificacion)
		{
			var cliente = _clienteRepository.ConsultarClienteIdentificacion(identificacion);
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (cliente == null)
				return NotFound(new ApiResponse<ICliente> { Success = false, Message = "Cliente no encontrado" });

			if (cliente.TipoCliente == TipoCliente.ClientePersona)
				return Ok(new ApiResponse<ClientePersona> { Success = true, Data = (ClientePersona)cliente });
			else
				return Ok(new ApiResponse<ClienteEmpresa> { Success = true, Data = (ClienteEmpresa)cliente });
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CrearPersona(string identificacion, int tipoId, string nombre, string primerApellido, string? segundoApellido, DateTime fechaNacimiento)
		{
			ValidarCliente validarPersona = new ValidarCliente();
			ValidationResult<ClientePersona> resultValidacion = validarPersona.ValidarPersona(identificacion, tipoId, nombre, primerApellido, segundoApellido, fechaNacimiento);

			if (!resultValidacion.ValidationSuccess)
			{
				ModelState.AddModelError("Error Validacion: ", resultValidacion.ValidationMessage);
				return BadRequest(ModelState);
			}

			 var cliente = _clienteRepository.CrearClientePersona(resultValidacion.ValidationData);

			if (cliente.Equals(null))
				return BadRequest(ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			return Ok(new ApiResponse<ClientePersona> { Success = true, Message= "Cliente creado exitosamente",  Data = cliente});
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CrearEmpresa(string identificacion, string nombre)
		{
			ValidarCliente validarEmpresa = new ValidarCliente();
			ValidationResult<ClienteEmpresa> resultValidacion = validarEmpresa.ValidarEmpresa(identificacion, nombre);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!string.IsNullOrEmpty(resultValidacion.ValidationMessage))
			{
				ModelState.AddModelError("Error Validacion: ", resultValidacion.ValidationMessage);
				return BadRequest(ModelState);
			}

			if (!resultValidacion.ValidationSuccess)
			{
				ModelState.AddModelError("Error Validacion: ", resultValidacion.ValidationMessage);
				return BadRequest(new ApiResponse<ClienteEmpresa>() { Success = false, Message = resultValidacion.ValidationMessage });
			}

			return Ok(new ApiResponse<ClienteEmpresa> { Success = true, Message = resultValidacion.ValidationMessage, Data = resultValidacion.ValidationData });

		}

	}
}
