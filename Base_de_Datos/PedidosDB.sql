-- AD01 - Comercio Multibodega
-- Microservicio: Pedidos.Api
-- Base de datos: PedidosDB
-- Script para SQL Server. Ejecutar en una instancia donde PedidosDB no exista.
-- IdProducto en DetallePedido es una referencia LOGICA a CatalogoDB.dbo.Producto:
-- no se crea FOREIGN KEY entre las bases independientes de los microservicios.

CREATE DATABASE [PedidosDB];
GO
USE [PedidosDB];
GO

CREATE TABLE [dbo].[Cliente] (
    [IdCliente] INT IDENTITY(1,1) NOT NULL,
    [Nombres] NVARCHAR(100) NOT NULL,
    [Cedula] NVARCHAR(10) NOT NULL,
    [Correo] NVARCHAR(100) NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY ([IdCliente])
);
GO

CREATE TABLE [dbo].[Pedido] (
    [IdPedido] INT IDENTITY(1,1) NOT NULL,
    [IdCliente] INT NOT NULL,
    [Fecha] DATETIME2(7) NOT NULL,
    [Total] DECIMAL(18,2) NOT NULL,
    [Estado] NVARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Pedido] PRIMARY KEY ([IdPedido]),
    CONSTRAINT [FK_Pedido_Cliente] FOREIGN KEY ([IdCliente])
        REFERENCES [dbo].[Cliente] ([IdCliente])
);
GO

CREATE TABLE [dbo].[DetallePedido] (
    [IdDetalle] INT IDENTITY(1,1) NOT NULL,
    [IdPedido] INT NOT NULL,
    [IdProducto] INT NOT NULL,
    [Cantidad] INT NOT NULL,
    [PrecioUnitario] DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_DetallePedido] PRIMARY KEY ([IdDetalle]),
    CONSTRAINT [FK_DetallePedido_Pedido] FOREIGN KEY ([IdPedido])
        REFERENCES [dbo].[Pedido] ([IdPedido])
);
GO

-- Datos exportados de PedidosDB; se conservan los ID originales.
SET IDENTITY_INSERT [dbo].[Cliente] ON;
INSERT INTO [dbo].[Cliente] ([IdCliente], [Nombres], [Cedula], [Correo]) VALUES
    (1, N'Juan Perez', N'1234567890', N'juan.prueba@example.com');
SET IDENTITY_INSERT [dbo].[Cliente] OFF;
GO

SET IDENTITY_INSERT [dbo].[Pedido] ON;
INSERT INTO [dbo].[Pedido] ([IdPedido], [IdCliente], [Fecha], [Total], [Estado]) VALUES
    (1, 1, CONVERT(DATETIME2(7), '2026-11-09T17:30:00.0000000', 126), 750.00, N'Pendiente');
SET IDENTITY_INSERT [dbo].[Pedido] OFF;
GO

-- DetallePedido no tenia registros en el script exportado.
