namespace Negohelp.Comun.Models
{
	#region Clientes
	/// <summary>
	/// Tipo Cliente, Persona o Empresa
	/// </summary>
	public enum TipoCliente
	{
		ClientePersona = 1,
		ClienteEmpresa = 2
	}
	/// <summary>
	/// Tipo Identificacion, cedula, ruc, pasaporte
	/// </summary>
	public enum TipoIdentificacion
	{
		Cedula = 1,
		RUC = 2,
		Pasaporte = 3
	}
	#endregion
}
