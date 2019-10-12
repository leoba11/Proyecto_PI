CREATE TABLE [dbo].[empleados] (
    [cedulaPK]           CHAR (9)      NOT NULL,
    [nombre]             VARCHAR (50)  NOT NULL,
    [apellido1]          VARCHAR (50)  NOT NULL,
    [apellido2]          VARCHAR (50)  NOT NULL,
    [edad]               AS            (CONVERT([int],datediff(day,[fechaNacimiento],getdate())/(365.25))),
    [fechaNacimiento]    DATE          NOT NULL,
    [telefono]           VARCHAR (MAX) NOT NULL,
    [provincia]          VARCHAR (50)  NOT NULL,
    [canton]             VARCHAR (50)  NOT NULL,
    [distrito]           VARCHAR (50)  NOT NULL,
    [correo]             VARCHAR (MAX) NULL,
    [direccionDetallada] VARCHAR (MAX) NULL,
    [disponibilidad]     BIT           CONSTRAINT [DF__empleados__dispo__787EE5A0] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_empleado] PRIMARY KEY CLUSTERED ([cedulaPK] ASC)
);


