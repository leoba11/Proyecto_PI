CREATE TABLE [dbo].[proyectos]
(
	[codigoPK] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [nombre] NCHAR(10) NULL, 
    [fechaInicio] DATETIME NULL, 
    [fechaEstimada] DATETIME NULL, 
    [costoEstimado] NCHAR(10) NOT NULL,  /*calcular*/
    [objetivo] NCHAR(10) NOT NULL, 
    [cedulaClienteFK] INT NOT NULL, 
    [idEquipo] VARCHAR(50) NULL
)
