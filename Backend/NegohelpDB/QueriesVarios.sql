use NH_Clientes
go
/* Crear Clientes

-- Persona
insert into Cliente (TipoCliente, Identificacion, TipoIdentificacion, Nombre, PrimerApellido, SegundoApellido, FechaNacimiento, LogFechaCreacion, Vigente)
values (0, '0615284652', 0, 'Juan Pablo', 'Vega', 'Reinoso', '2005-06-29', GETDATE(),1 )

-- Empresa
insert into Cliente (TipoCliente, Identificacion, TipoIdentificacion, Nombre, LogFechaCreacion, Vigente)
values (1, '1756985468001', 1, 'Carpinteria Roca', GETDATE(),1 )
*/

select * from Cliente

--update Cliente
--set TipoIdentificacion = 1

--update Cliente
--set Vigente = 0
--where Id = 3

exec ConsultarClienteIdentificacion '0615284652'

declare @id int

EXEC CrearClientePersona 
    @Identificacion = '1712556698',
    @TipoIdentificacion = 1,
    @Nombre = 'MA',
    @PrimerApellido = 'Rosal',
    @SegundoApellido = 'Flores',
    @FechaNacimiento = '1986-06-25',
    @IdCliente = @id OUTPUT

select @id