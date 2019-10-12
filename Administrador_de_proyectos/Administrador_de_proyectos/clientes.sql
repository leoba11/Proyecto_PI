CREATE TABLE [dbo].[clientes] (
    [cedulaPK]           CHAR (9)      NOT NULL,
    [nombre]             VARCHAR (50)  NOT NULL,
    [apellido1]          VARCHAR (50)  NOT NULL,
    [apellido2]          VARCHAR (50)  NOT NULL,
    [telefono]           VARCHAR (MAX) NOT NULL,
    [provincia]          VARCHAR (50)  NOT NULL,
    [canton]             VARCHAR (50)  NOT NULL,
    [distrito]           VARCHAR (50)  NOT NULL,
    [correo]             VARCHAR (MAX) NULL,
    [direccionDetallada] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED ([cedulaPK] ASC)
);

