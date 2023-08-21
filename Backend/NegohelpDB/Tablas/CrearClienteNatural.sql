
USE NH_Clientes
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Marcos Torres>
-- Create date: 26 Julio 2023>
-- Description:	Crear nuevo ciente natural
-- =============================================
CREATE PROCEDURE CrearClienteNatural
	-- Add the parameters for the stored procedure here
	@TipoCliente bit = 0,
	@Identificacion nchar(20),
	@TipoIdentificacion int,
	@Nombre nchar (50),
	@PrimerApellido nchar (40),
	@SegundoApellido nchar (40),
	@FechaNacimiento datetime,
	@LogFechaCreacion datetime,
	@IdCliente int output

AS
BEGIN
	declare @idGenerado Table(ID int)

	INSERT INTO Clientes (TipoCliente, Identificacion, TipoIdentificacion, Nombre, PrimerApellido, SegundoApellido, FechaNacimiento, LogFechaCreacion, Vigente)
    OUTPUT inserted.ID INTO @IdGenerado
    VALUES (@TipoCliente, @Identificacion, @TipoIdentificacion, 	@Nombre, @PrimerApellido, @SegundoApellido, 	@FechaNacimiento, @LogFechaCreacion, 1);
	
	select @IdCliente = ID from @idGenerado

END
GO

