CREATE TABLE [dbo].[requerimientos] (
    [codigoProyectoFK] INT          NOT NULL,
    [idModuloFK]       INT          NOT NULL,
    [idPK]             INT          IDENTITY (1, 1) NOT NULL,
    [descripcion]      VARCHAR (50) NOT NULL,
    [complejidad]      INT          DEFAULT ((1)) NOT NULL,
    [estado]           VARCHAR (50) DEFAULT ('No iniciado') NOT NULL,
    [cedulaEmpleadoFK] CHAR (9)     NOT NULL,
    [fechaInicio]      DATETIME     NOT NULL,
    [fechaFin]         DATETIME     NULL,
    [duraciónEstimada] DATETIME     NOT NULL,
    [duraciónReal]     DATETIME     NULL,
    CONSTRAINT [PK_requerimientos] PRIMARY KEY CLUSTERED ([codigoProyectoFK] ASC, [idModuloFK] ASC, [idPK] ASC),
    CONSTRAINT [FK_proyecto_modulo] FOREIGN KEY ([codigoProyectoFK], [idModuloFK]) REFERENCES [dbo].[modulos] ([codigoProyectoFK], [idPK]) ON DELETE CASCADE ON UPDATE CASCADE
);


