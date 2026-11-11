-- AD01 - Comercio Multibodega
-- Microservicio: Inventario.Api
-- Base de datos: Inventaria_DB (nombre utilizado realmente en el proyecto).
-- Script para SQL Server. Ejecutar en una instancia donde Inventaria_DB no exista.
-- IdProducto es una referencia LOGICA a CatalogoDB.dbo.Producto:
-- no se crea FOREIGN KEY entre las bases independientes de los microservicios.

CREATE DATABASE [Inventaria_DB];
GO
USE [Inventaria_DB];
GO

CREATE TABLE [dbo].[Bodega] (
    [IdBodega] INT IDENTITY(1,1) NOT NULL,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Ciudad] NVARCHAR(100) NOT NULL,
    [Direccion] NVARCHAR(200) NULL,
    CONSTRAINT [PK_Bodega] PRIMARY KEY ([IdBodega])
);
GO

CREATE TABLE [dbo].[Existencia] (
    [IdExistencia] INT IDENTITY(1,1) NOT NULL,
    [IdProducto] INT NOT NULL,
    [IdBodega] INT NOT NULL,
    [Stock] INT NOT NULL,
    CONSTRAINT [PK_Existencia] PRIMARY KEY ([IdExistencia]),
    CONSTRAINT [FK_Existencia_Bodega] FOREIGN KEY ([IdBodega])
        REFERENCES [dbo].[Bodega] ([IdBodega])
);
GO

CREATE TABLE [dbo].[MovimientoInventario] (
    [IdMovimiento] INT IDENTITY(1,1) NOT NULL,
    [IdProducto] INT NOT NULL,
    [IdBodega] INT NOT NULL,
    [Tipo] NVARCHAR(50) NOT NULL,
    [Cantidad] INT NOT NULL,
    [Fecha] DATETIME2(7) NOT NULL,
    CONSTRAINT [PK_MovimientoInventario] PRIMARY KEY ([IdMovimiento]),
    CONSTRAINT [FK_MovimientoInventario_Bodega] FOREIGN KEY ([IdBodega])
        REFERENCES [dbo].[Bodega] ([IdBodega])
);
GO

CREATE TABLE [dbo].[ReservaStock] (
    [IdReserva] INT IDENTITY(1,1) NOT NULL,
    [IdProducto] INT NOT NULL,
    [IdBodega] INT NOT NULL,
    [Cantidad] INT NOT NULL,
    [Estado] NVARCHAR(50) NOT NULL,
    CONSTRAINT [PK_ReservaStock] PRIMARY KEY ([IdReserva]),
    CONSTRAINT [FK_ReservaStock_Bodega] FOREIGN KEY ([IdBodega])
        REFERENCES [dbo].[Bodega] ([IdBodega])
);
GO

-- Datos exportados de Inventaria_DB; se conservan los ID originales.
SET IDENTITY_INSERT [dbo].[Bodega] ON;
INSERT INTO [dbo].[Bodega] ([IdBodega], [Nombre], [Ciudad], [Direccion]) VALUES
    (1, N'Bodega Principal', N'Quito', N'Av. Amazonas'),
    (3, N'Bodega Principal', N'Quito', N'Av. Amazonas');
SET IDENTITY_INSERT [dbo].[Bodega] OFF;
GO

SET IDENTITY_INSERT [dbo].[Existencia] ON;
INSERT INTO [dbo].[Existencia] ([IdExistencia], [IdProducto], [IdBodega], [Stock]) VALUES
    (1, 1, 1, 12);
SET IDENTITY_INSERT [dbo].[Existencia] OFF;
GO

SET IDENTITY_INSERT [dbo].[MovimientoInventario] ON;
INSERT INTO [dbo].[MovimientoInventario]
    ([IdMovimiento], [IdProducto], [IdBodega], [Tipo], [Cantidad], [Fecha])
VALUES
    (1, 1, 3, N'Entrada', 15, CONVERT(DATETIME2(7), '2026-11-10T17:30:00.0000000', 126));
SET IDENTITY_INSERT [dbo].[MovimientoInventario] OFF;
GO

-- ReservaStock no tenia registros en el script exportado.
