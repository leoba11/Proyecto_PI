CREATE TABLE [dbo].[roles] (
    [rol]              VARCHAR (25) DEFAULT ('Desarrollador') NOT NULL,
    [cedulaFK]         CHAR (9)     NOT NULL,
    [codigoProyectoFK] INT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_rol] PRIMARY KEY CLUSTERED ([cedulaFK] ASC, [codigoProyectoFK] ASC),
    CONSTRAINT [FK_cedulaEmpleado] FOREIGN KEY ([cedulaFK]) REFERENCES [dbo].[empleados] ([cedulaPK]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_codProyecto] FOREIGN KEY ([codigoProyectoFK]) REFERENCES [dbo].[proyectos] ([codigoPK]) ON DELETE SET DEFAULT ON UPDATE CASCADE
);


