using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negohelp.Comun.ApiHelpers;
using Negohelp.Comun.Models;
using Negohelp.IRepository;
using System.ComponentModel.DataAnnotations;

namespace Negohelp.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ClienteController : Controller
	{
		private readonly IClienteRepository _clienteRepository;
		private readonly IMapper _mapper;

		public ClienteController(IClienteRepository clienteRepository, IMapper mapper)
		{
			_clienteRepository = clienteRepository;
			_mapper = mapper;
		}

		[HttpGet, Authorize]
		[ProducesResponseType(200, Type = typeof(ICliente))]
		public IActionResult ConsultarClienteIdentificacion([Required] string identificacion)
		{
			var cliente = _clienteRepository.ConsultarClienteIdentificacion(identificacion);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (cliente == null)
				return NotFound(new ApiResponse<ICliente> { Success = false, Message = "Cliente no encontrado" });

			if (cliente.TipoCliente == TipoCliente.ClientePersona)
			{
				ClientePersonaDto clientePersonaDto = _mapper.Map<ClientePersona, ClientePersonaDto>((ClientePersona)cliente);
				return Ok(new ApiResponse<ClientePersonaDto> { Success = true, Data = clientePersonaDto });
			}
			else
			{
				ClienteEmpresaDto cienteEmpresaDto = _mapper.Map<ClienteEmpresa, ClienteEmpresaDto>((ClienteEmpresa)cliente);
				return Ok(new ApiResponse<ClienteEmpresaDto> { Success = true, Data = cienteEmpresaDto });
			}
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(ICliente))]
		public IActionResult ConsultarClienteNombre([Required] string nombre)
		{
			List<ListaClientes> clientes = _clienteRepository.ConsultarClienteNombre(nombre);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (clientes == null)
				return NotFound(new ApiResponse<ICliente> { Success = false, Message = "Cliente no encontrado" });

			return Ok(new ApiResponse<List<ListaClientes>> { Success = true, Data = clientes });
		}


		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CrearPersona(ClientePersonaDto persona)
		{
			DataValidation validar = new DataValidation();
			var cliente = _mapper.Map<ClientePersonaDto, ClientePersona>(persona);


			ValidationResult<ClientePersona> resultValidacion = validar.ValidarPersona(cliente);

			if (!resultValidacion.ValidationSuccess)
			{
				ModelState.AddModelError("Error Validacion: ", resultValidacion.ValidationMessage);
				return BadRequest(ModelState);
			}

			var clientePersona = _mapper.Map<ClientePersona, ClientePersonaDto>(_clienteRepository.CrearClientePersona(resultValidacion.ValidationData));

			if (cliente.Equals(null))
				return BadRequest(ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			return Ok(new ApiResponse<ClientePersonaDto> { Success = true, Message = "Cliente creado exitosamente", Data = clientePersona });
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CrearEmpresa(ClienteEmpresaDto empresa)
		{
			DataValidation validarEmpresa = new DataValidation();
			ClienteEmpresa cliente = _mapper.Map<ClienteEmpresaDto, ClienteEmpresa>(empresa);

			ValidationResult<ClienteEmpresa> resultValidacion = validarEmpresa.ValidarEmpresa(cliente.Identificacion, cliente.Nombre);

			if (!resultValidacion.ValidationSuccess)
			{
				ModelState.AddModelError("Error Validacion: ", resultValidacion.ValidationMessage);
				return BadRequest(ModelState);
			}

			var clienteEmpresa = _mapper.Map<ClienteEmpresa, ClienteEmpresaDto>(_clienteRepository.CrearClienteEmpresa(resultValidacion.ValidationData));

			if (clienteEmpresa.Equals(null))
				return BadRequest(ModelState);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(new ApiResponse<ClienteEmpresaDto> { Success = true, Message = "Cliente creado exitosamente", Data = clienteEmpresa });
		}

	}
}
