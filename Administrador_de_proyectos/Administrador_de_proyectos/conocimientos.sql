CREATE TABLE [dbo].[conocimientos] (
    [cedulaEmpleadoFK] CHAR (9)      NOT NULL,
    [conocimientoPK]   NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_conocimientos] PRIMARY KEY CLUSTERED ([cedulaEmpleadoFK] ASC, [conocimientoPK] ASC),
    CONSTRAINT [cedulaEmpleadoFK] FOREIGN KEY ([cedulaEmpleadoFK]) REFERENCES [dbo].[empleados] ([cedulaPK]) ON DELETE CASCADE ON UPDATE CASCADE
);


