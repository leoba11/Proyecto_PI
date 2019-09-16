CREATE TABLE [dbo].[requerimientos]
(
	[codigoproyectoFK] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [nombreModuloFK] VARCHAR(50) NOT NULL PRIMARY KEY, 
	[idPK] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [descripcion] VARCHAR(50) NOT NULL, 
    [peso] INT NOT NULL, 
    [estado] VARCHAR(50) NOT NULL, 
    [cedulaEmpleadoFK] VARCHAR(50) NOT NULL, 
)
