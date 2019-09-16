CREATE TABLE [dbo].[conocimientos]
(
	[cedulaEmpleadoFK] INT NOT NULL ,
	[conocimientoPK] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_conocimientos] PRIMARY KEY ([cedulaEmpleadoFK], [conocimientoPK]) 
)
