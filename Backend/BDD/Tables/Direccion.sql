USE [NH_Clientes]
GO

/****** Object:  Table [dbo].[Direccion]    Script Date: 2023-08-07 10:52:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.Direccion(
	Id int Identity(1,1) NOT NULL CONSTRAINT PK_Id_Direccion PRIMARY KEY CLUSTERED,
	Cliente_Id int NOT NULL FOREIGN KEY REFERENCES Cliente(Id) ,
	TipoDireccion int NOT NULL,
	Direccion varchar(250) NOT NULL,
	Principal bit NOT NULL,
	Vigente bit NOT NULL
) ON [PRIMARY]
GO
/* Index */
Create Index ix_cliente_id ON Direccion(Cliente_Id)

/*
drop table Direccion
*/