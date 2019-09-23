CREATE TABLE [dbo].[proyectos]
(
	[codigoPK] VARCHAR(50) NOT NULL, 
    [nombre] VARCHAR(50) NOT NULL, 
    [fechaInicio] DATETIME NOT NULL, 
    [fechaEstimada] DATETIME NOT NULL, 
    [costoEstimado] MONEY NOT NULL,  
    [objetivo] VARCHAR(MAX) NOT NULL, 
    [cedulaClienteFK] INT NOT NULL, 
    [idEquipo] VARCHAR(50) NULL,

	CONSTRAINT [PK_proyecto] PRIMARY KEY ([codigoPK]),
	CONSTRAINT FK_cedulaCliente FOREIGN KEY ([cedulaClienteFK])
	REFERENCES dbo.[clientes]([cedulaPK]) ON DELETE SET NULL ON UPDATE CASCADE,
)
