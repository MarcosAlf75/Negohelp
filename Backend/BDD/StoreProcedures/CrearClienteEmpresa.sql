-- =============================================
-- Author:		Marcos Torres>
-- Create date: 26 Julio 2023>
-- Description:	Crear nuevo ciente natural
-- =============================================
-- drop procedure CrearClienteEmpresa
USE Negohelp
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE CrearClienteEmpresa
	@TipoCliente int,
	@Identificacion nchar(20),
	@TipoIdentificacion int,
	@Nombre nchar (50),
	@IdCliente int output

AS
BEGIN
	declare @idGenerado Table(ID int)

	INSERT INTO Cliente (
		TipoCliente,
		Identificacion,
		TipoIdentificacion,
		Nombre,
		LogFechaCreacion,
		LogFechaActualizacion, 
		Vigente)
    OUTPUT inserted.ID INTO @IdGenerado
    VALUES (
		@TipoCliente,
		LTRIM(RTRIM(@Identificacion)),
		@TipoIdentificacion,
		@Nombre,
		GETDATE(),
		GETDATE(),
		1);
	
	select @IdCliente = ID from @idGenerado

END
GO



