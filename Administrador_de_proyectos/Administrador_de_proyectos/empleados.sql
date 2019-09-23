CREATE TABLE [dbo].[empleados]
(
	[cedulaPK] INT NOT NULL PRIMARY KEY,
	[rol] VARCHAR(50) NOT NULL,
	[nombre] VARCHAR(50) NOT NULL,
	[apellido1] VARCHAR(50) NOT NULL,
	[apellido2] VARCHAR(50) NOT NULL, 
    [edad] AS cast(datediff(dd, [fechaNacimiento] ,GETDATE()) / 365.25 as int), /*verificar el calculo*/ 
    [fechaNacimiento] DATE NOT NULL, 
    [telefono1] INT NOT NULL, 
    [telefono2] INT NULL, 
    [provincia] VARCHAR(50) NOT NULL, 
    [canton] VARCHAR(50) NOT NULL, 
    [distrito] VARCHAR(50) NOT NULL, 
    [correo] VARCHAR(MAX) NULL, 
	[disponibilidad] VARCHAR(50) NOT NULL,  /*agregar el calculo de la condicion*/
	[codigoProyectoFK] VARCHAR(50) NULL,
	CONSTRAINT codigoProyectoFK FOREIGN KEY ([codigoProyectoFK])
	REFERENCES dbo.[proyectos]([codigoPK]) ON DELETE SET NULL ON UPDATE CASCADE, /* cambiar por algun default*/
)
