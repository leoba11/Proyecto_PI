CREATE TABLE [dbo].[modulos] (
    [codigoProyectoFK] INT           NOT NULL,
    [idPK]             INT           IDENTITY (1, 1) NOT NULL,
    [nombre]           VARCHAR (50)  NOT NULL,
    [descripcion]      VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_modulos] PRIMARY KEY CLUSTERED ([codigoProyectoFK] ASC, [idPK] ASC),
    CONSTRAINT [FK_codigoProyecto] FOREIGN KEY ([codigoProyectoFK]) REFERENCES [dbo].[proyectos] ([codigoPK]) ON DELETE CASCADE ON UPDATE CASCADE
);


