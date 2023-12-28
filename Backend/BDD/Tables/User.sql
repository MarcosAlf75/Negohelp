USE Autenticacion
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author, Marcos Torres>
-- Create date: <Create Date, Octubre 22, 2023>
-- Description:	<Description, Registrar o Crear usuario>
-- =============================================

CREATE TABLE Users (
    Id int identity(1,1) not null constraint PK_Id_UserId primary key clustered,
    UserName varchar(50) NOT NULL,
    PasswordH varchar(100) NOT NULL,
	Email varchar(50) NULL,
	FechaCreacion datetime Not null,
	Vigente bit not null
	)

--drop table Users