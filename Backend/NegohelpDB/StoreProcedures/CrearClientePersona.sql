USE [NH_Clientes]
GO
/****** Object:  StoredProcedure [dbo].[CrearClienteNatural]    Script Date: 2023-08-08 3:58:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Marcos Torres>
-- Create date: 8 de Agosto 2023
-- Description:	Crear nuevo ciente persona
-- =============================================
-- drop procedure CrearClientePersona

CREATE PROCEDURE CrearClientePersona
	-- Add the parameters for the stored procedure here
	@TipoCliente int,
	@Identificacion nchar(20),
	@TipoIdentificacion int,
	@Nombre nchar (50),
	@PrimerApellido nchar (40),
	@SegundoApellido nchar (40) NULL,
	@FechaNacimiento datetime,
	@IdCliente int output

AS
BEGIN
	declare @idGenerado Table(ID int)

	INSERT INTO Cliente (	TipoCliente,
							Identificacion,
							TipoIdentificacion,
							Nombre,
							PrimerApellido,
							SegundoApellido,
							FechaNacimiento,
							LogFechaCreacion,
							LogFechaActualizacion,
							Vigente)
    OUTPUT inserted.ID
	INTO @IdGenerado VALUES (@TipoCliente,
							@Identificacion,
							@TipoIdentificacion,
							@Nombre,
							@PrimerApellido,
							@SegundoApellido,
							@FechaNacimiento,
							GETDATE(),
							GETDATE(),
							1);
	
	select @IdCliente = ID from @idGenerado

END
go

