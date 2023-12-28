USE Autenticacion
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author, Marcos Torres>
-- Create date: <Create Date, Octubre 22, 2023>
-- Description:	<Crear usuario para autenticar
--               el ingreso al aplicativo>
-- =============================================
CREATE PROCEDURE  [dbo].[RegistrarUsuario]  
	@UserName varchar(15),
	@Password varchar(100),
	@Email varchar(50)
AS
BEGIN

	insert into Users (
		UserName,
		PasswordH,
		Email,
		FechaCreacion,
		Vigente
	)
	values (
		@UserName,
		@Password,
		@Email,
		GetDate(),
		1
	)

	if (@@Error = 0)
		return 0

END
GO

--drop proc [RegistrarUsuario]