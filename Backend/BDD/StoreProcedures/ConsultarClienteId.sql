USE
Clientes
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcos Torres
-- Create date: 26 Julio 2023
-- Description:	Consultar Cliente por Id
-- =============================================
CREATE PROCEDURE ConsultarClienteId
	@Id int
AS
BEGIN
	
	Select 	Id,
			TipoCliente,
			TRIM(Identificacion) as Identificacion,
			TipoIdentificacion,
			TRIM(Nombre) as Nombre,
			TRIM(PrimerApellido) as PrimerApellido,
			TRIM(SegundoApellido) as SegundoApellido,
			FechaNacimiento,
			LogFechaCreacion,
			LogFechaActualizacion,
			LogFechaEliminacion,
			Vigente
	from Cliente
	Where Id = @Id
	and Vigente = 1

END
GO

/*
drop procedure ConsultarClienteId
*/