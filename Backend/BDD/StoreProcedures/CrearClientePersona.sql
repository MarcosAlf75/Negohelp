-- =============================================  
-- Author:  Marcos Torres>  
-- Create date: 8 de Agosto 2023  
-- Description: Crear nuevo ciente persona  
-- =============================================  
-- drop procedure CrearClientePersona 

USE Negohelp
GO
  
CREATE PROCEDURE CrearClientePersona  
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
  
 INSERT INTO Cliente ( TipoCliente,  
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
       LTRIM(RTRIM(@Identificacion)),  
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