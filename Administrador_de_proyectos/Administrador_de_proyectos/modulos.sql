CREATE TABLE [dbo].[modulos]
(
	[codigoproyectoFK] VARCHAR(50) NOT NULL , 
    [nombrePK] VARCHAR(50) NOT NULL , 
    [descripcion] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_modulos] PRIMARY KEY ([codigoproyectoFK], [nombrePK])
)
