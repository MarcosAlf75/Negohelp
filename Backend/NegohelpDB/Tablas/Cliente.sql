USE [NH_Clientes]
GO

/****** Object:  Table [dbo].[cl_clientes]    Script Date: 2023-07-19 1:03:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	Id int Identity(1,1) NOT NULL CONSTRAINT PK_Id_Cliente PRIMARY KEY CLUSTERED,
	TipoCliente smallint NOT NULL,
	Identificacion varchar(20) NOT NULL,
	TipoIdentificacion smallint NOT NULL,
	Nombre varchar(50),
	PrimerApellido varchar(40),
	SegundoApellido varchar(40),
	FechaNacimiento date,
	LogFechaCreacion datetime NOT NULL,
	LogFechaActualizacion datetime NOT NULL,
	LogFechaEliminacion datetime,
	Vigente bit NOT NULL
) ON [PRIMARY]
GO

/* Index */

Create INDEX ix_cliente_identificacion ON Cliente(Identificacion)
Create INDEX ix_apellidos ON Cliente (PrimerApellido, SegundoApellido)

/*
drop table Cliente
*/