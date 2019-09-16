CREATE TABLE [dbo].[requerimientos]
(
	[codigoproyectoFK] VARCHAR(50) NOT NULL , 
    [nombreModuloFK] VARCHAR(50) NOT NULL , 
	[idPK] VARCHAR(50) NOT NULL , 
    [descripcion] VARCHAR(50) NOT NULL, 
    [peso] INT NOT NULL, 
    [estado] VARCHAR(50) NOT NULL, 
    [cedulaEmpleadoFK] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_requerimientos] PRIMARY KEY ([codigoproyectoFK], [nombreModuloFK], [idPK]), 
)
