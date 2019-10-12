CREATE TABLE [dbo].[proyectos] (
    [codigoPK]           INT           IDENTITY (1, 1) NOT NULL,
    [nombre]             VARCHAR (50)  NOT NULL,
    [fechaInicio]        DATETIME      NOT NULL,
    [fechaFinalEstimada] DATETIME      NOT NULL,
    [costoEstimado]      MONEY         NOT NULL,
    [objetivo]           VARCHAR (MAX) NULL,
    [cedulaClienteFK]    CHAR (9)      DEFAULT ((0)) NOT NULL,
    [idEquipo]           INT           NULL,
    [fechaFinal]         DATETIME      NULL,
    [costoReal]          MONEY         NULL,
    CONSTRAINT [PK_proyecto] PRIMARY KEY CLUSTERED ([codigoPK] ASC),
    CONSTRAINT [FK_cedulaCliente] FOREIGN KEY ([cedulaClienteFK]) REFERENCES [dbo].[clientes] ([cedulaPK]) ON DELETE SET DEFAULT ON UPDATE CASCADE
);


