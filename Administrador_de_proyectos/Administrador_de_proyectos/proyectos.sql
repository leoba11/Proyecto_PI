CREATE TABLE [dbo].[proyectos]
(
	[codigoPK] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [nombre] VARCHAR(50) NOT NULL, 
    [fechaInicio] DATETIME NOT NULL, 
    [fechaEstimada] DATETIME NOT NULL, 
    [costoEstimado] MONEY NOT NULL,  /*calcular con base en fechas*/
    [objetivo] VARCHAR(MAX) NOT NULL, 
    [cedulaClienteFK] INT NOT NULL, 
    [idEquipo] VARCHAR(50) NULL
)
