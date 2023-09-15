USE [NH_Clientes]
GO

/****** Object:  Table [dbo].[Telefonos]    Script Date: 2023-08-05 5:52:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.Telefono(
	Id int Identity(1,1) NOT NULL CONSTRAINT PK_Id_Telefono PRIMARY KEY CLUSTERED,
	Cliente_Id int NOT NULL FOREIGN KEY REFERENCES Cliente(Id) ,
	TipoTelefono int NOT NULL,
	Telefono varchar(15) NOT NULL,
	Principal bit NOT NULL,
	Vigente bit NOT NULL
) ON [PRIMARY]
GO
/* Index */
Create Index ix_cliente_id ON Telefono(Cliente_Id)

/* drop Telefono
drop table Telefono
*/

-- sp_help Telefono