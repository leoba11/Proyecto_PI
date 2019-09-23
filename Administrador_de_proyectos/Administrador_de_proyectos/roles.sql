﻿CREATE TABLE [dbo].[roles]
(
	[rol] VARCHAR(50) NOT NULL DEFAULT 'desarrollador',
	[cedulaFK] INT NOT NULL,
	[codigoProyectoFK] VARCHAR(50) NOT NULL,

	CONSTRAINT [PK_rol] PRIMARY KEY ([cedulaFK], [codigoProyectoFK]),
	CONSTRAINT FK_codProyecto FOREIGN KEY ([codigoProyectoFK])
	REFERENCES dbo.[proyectos]([codigoPK]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_cedulaEmpleado FOREIGN KEY ([cedulaFK])
	REFERENCES dbo.[empleados]([cedulaPK]) ON DELETE SET NULL ON UPDATE CASCADE,
)