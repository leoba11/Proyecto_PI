CREATE TABLE [dbo].[modulos]
(
	[codigoProyectoFK] VARCHAR(50) NOT NULL , 
    [nombrePK] VARCHAR(50) NOT NULL , 
    [descripcion] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_modulos] PRIMARY KEY ([codigoProyectoFK], [nombrePK]),
	CONSTRAINT FK_codigoProyecto FOREIGN KEY ([codigoProyectoFK])
	REFERENCES dbo.[proyectos]([codigoPK]) ON DELETE CASCADE ON UPDATE CASCADE,
)
