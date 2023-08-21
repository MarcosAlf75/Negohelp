
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcos Torres
-- Create date: 26 Julio 2023
-- Description:	Consultar Cliente por Id
-- =============================================
CREATE PROCEDURE ConsultarClienteIdentificacion
	@Identificacion varchar(20)
AS
BEGIN
	
	Select 	Id,
			TipoCliente,
			TRIM(Identificacion),
			TipoIdentificacion,
			TRIM(Nombre),
			TRIM(PrimerApellido),
			TRIM(SegundoApellido),
			TRIM(NombreComercial),
			TRIM(RazonSocial),
			FechaNacimiento,
			LogFechaCreacion,
			LogFechaActualizacion,
			LogFechaEliminacion,
			Vigente
	from Clientes
	Where Identificacion = TRIM(@Identificacion)

END
GO