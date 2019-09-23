CREATE TABLE [dbo].[requerimientos]
(
	[codigoProyectoFK] VARCHAR(50) NOT NULL , 
    [nombreModuloFK] VARCHAR(50) NOT NULL , 
	[idPK] VARCHAR(50) NOT NULL , 
    [descripcion] VARCHAR(50) NOT NULL, 
    [peso] INT NOT NULL, 
    [estado] VARCHAR(50) NOT NULL, 
    [cedulaEmpleadoFK] CHAR(10) NOT NULL, 
    CONSTRAINT [PK_requerimientos] PRIMARY KEY ([codigoProyectoFK], [nombreModuloFK], [idPK]), 
	CONSTRAINT FK_proyecto_modulo FOREIGN KEY ([codigoProyectoFK], [nombreModuloFK])
	REFERENCES dbo.[modulos]([codigoProyectoFK], [nombrePK]) ON DELETE CASCADE ON UPDATE CASCADE,
)
