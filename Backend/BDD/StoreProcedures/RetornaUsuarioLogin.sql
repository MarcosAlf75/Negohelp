USE Autenticacion
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author, Marcos Torres>
-- Create date: <Create Date, Octubre 22, 2023>
-- Description:	<Retorna usuaio y password para validacion en el login>
-- =============================================
CREATE PROCEDURE  [dbo].RetonaUsuarioLogin  
	@UserName varchar(15)

AS
BEGIN

	Select	UserName,
			PasswordH
	from Users
	where UserName = @UserName
	  and Vigente = 1

	if (@@Error = 0)
		return 0

END
GO
