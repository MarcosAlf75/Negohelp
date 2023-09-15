-- =============================================
-- Author:		Marcos Torres
-- Create date: 26 Julio 2023
-- Description:	Consultar Cliente por Id
-- =============================================
-- drop procedure ConsultarClienteNombre

USE 
Clientes
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE ConsultarClienteNombre
	@nombre varchar(50)
AS
BEGIN
	
	Select 	Id,
			TipoCliente,
			TRIM(Identificacion) as Identificacion,
			TipoIdentificacion,
			Case
				when TipoCliente = 2 then TRIM(Nombre)
				else TRIM(Nombre) + ' ' + TRIM(PrimerApellido) + ' ' + TRIM(SegundoApellido)
			End	as Nombre
	from Cliente
	Where nombre like @nombre + '%'
	or PrimerApellido like @nombre + '%'
	or SegundoApellido like @nombre + '%'
	and Vigente = 1

END
GO
