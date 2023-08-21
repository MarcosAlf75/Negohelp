﻿using Negohelp.Comun.Models;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Negohelp.ValidarDatos
{
	public class ValidarCliente
	{
		private string mensaje = string.Empty;

		public ValidationResult<ClientePersona> ValidarPersona(string identificacion, int tipoId, string nombre, string primerApellido, string? segundoApellido, DateTime fechaNacimiento)
		{
			if (!Enum.IsDefined(typeof(TipoIdentificacion), tipoId))
			{
				mensaje = "Tipo de identificación debe ser Cedula = 1, RUC = 2, Pasaporte = 3";
				return new ValidationResult<ClientePersona> { ValidationSuccess = false, ValidationMessage = mensaje };
			}

			if (!ValidarIdentificacion(identificacion, TipoCliente.ClientePersona, (TipoIdentificacion)tipoId))
				return new ValidationResult<ClientePersona> { ValidationSuccess = false, ValidationMessage = mensaje };

			if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(primerApellido))
			{
				mensaje = "Nombre y primer apellido son mandatorios";
				return new ValidationResult<ClientePersona> { ValidationSuccess = false, ValidationMessage = mensaje };
			}

			ClientePersona persona = new ClientePersona()
			{
				Identificacion = identificacion,
				TipoCliente = TipoCliente.ClientePersona,
				TipoIdentificacion = (TipoIdentificacion)tipoId,
				Nombre = nombre,
				PrimerApellido = primerApellido,
				SegundoApellido = string.IsNullOrEmpty(segundoApellido) ? "" : segundoApellido,
				FechaNacimiento = fechaNacimiento,
				Vigente = true
			};

			return new ValidationResult<ClientePersona>() { ValidationSuccess = true, ValidationMessage = mensaje, ValidationData = persona };
		}
		public ValidationResult<ClienteEmpresa> ValidarEmpresa(string identificacion,  string nombre)
		{

			if (!ValidarIdentificacion(identificacion, TipoCliente.ClienteEmpresa, TipoIdentificacion.RUC))
				return new ValidationResult<ClienteEmpresa> { ValidationSuccess = false, ValidationMessage = mensaje };

			if (string.IsNullOrEmpty(nombre))
			{
				mensaje = "Nombre de empresa es mandatorio";
				return new ValidationResult<ClienteEmpresa> { ValidationSuccess = false, ValidationMessage = mensaje };
			}

			ClienteEmpresa empresa = new ClienteEmpresa()
			{
				Identificacion = identificacion,
				TipoCliente = TipoCliente.ClienteEmpresa,
				TipoIdentificacion = TipoIdentificacion.RUC,
				Nombre = nombre,
				Vigente = true
			};

			return new ValidationResult<ClienteEmpresa>() { ValidationSuccess = true, ValidationMessage = mensaje, ValidationData = empresa };
		}
		/// <summary>
		/// Valida el formato de identificacion para empresas o personas
		/// </summary>
		/// <param name="identificacion">Numero de identificacion</param>
		/// <param name="tipoCliente">Identifica si es persona o empresa (enumerador)</param>
		/// <param name="tipoID">Tipo identificacion cedula, ruc o pasaporte (enumerador)</param>
		/// <returns></returns>
		internal bool ValidarIdentificacion(string identificacion, TipoCliente tipoCliente, TipoIdentificacion tipoID)
		{
			if (string.IsNullOrEmpty(identificacion))
			{
				mensaje = "Identificacion no puede ser nula o vacia";
				return false;
			}

			if (TipoCliente.ClienteEmpresa == tipoCliente && (TipoIdentificacion.Pasaporte == tipoID || TipoIdentificacion.Cedula == tipoID))
			{
				mensaje = "Empresa no puede tener identificacion cedula o pasaporte";
				return false;
			}

			if ((TipoIdentificacion.RUC == tipoID || TipoIdentificacion.Cedula == tipoID) && !identificacion.All(char.IsDigit))
			{
				mensaje = "Identificacion debe tener solamente digitos";
				return false;
			}

			if (TipoIdentificacion.RUC == tipoID && identificacion.Length != 13)
			{
				mensaje = "Identificacion tipo RUC debe tener 13 digitos";
				return false;
			}

			if (TipoIdentificacion.Cedula == tipoID && identificacion.Length != 10)
			{
				mensaje = "Identificacion tipo Cedula debe tener 10 digitos";
				return false;
			}

			return true;
		}
	}
}
