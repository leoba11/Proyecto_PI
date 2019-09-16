CREATE TABLE [dbo].[conocimientos]
(
	[cedulaEmpleadoFK] INT NOT NULL ,
	[conocimientoPK] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_conocimientos] PRIMARY KEY ([cedulaEmpleadoFK], [conocimientoPK]) ,
	CONSTRAINT cedulaEmpleadoFK FOREIGN KEY ([cedulaEmpleadoFK])
	REFERENCES dbo.[empleados]([cedulaPK]) ON DELETE CASCADE,
)
