-- AD01 - Comercio Multibodega
-- Microservicio: Catalogo.Api
-- Base de datos: CatalogoDB
-- Script para SQL Server. Ejecutar en una instancia donde CatalogoDB no exista.
-- Los identificadores se conservan para coincidir con Inventaria_DB y PedidosDB.

CREATE DATABASE [CatalogoDB];
GO
USE [CatalogoDB];
GO

CREATE TABLE [dbo].[Categoria] (
    [IdCategoria] INT IDENTITY(1,1) NOT NULL,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Descripcion] NVARCHAR(200) NULL,
    CONSTRAINT [PK_Categoria] PRIMARY KEY ([IdCategoria])
);
GO

CREATE TABLE [dbo].[Marca] (
    [IdMarca] INT IDENTITY(1,1) NOT NULL,
    [Nombre] NVARCHAR(100) NOT NULL,
    [PaisOrigen] NVARCHAR(100) NULL,
    CONSTRAINT [PK_Marca] PRIMARY KEY ([IdMarca])
);
GO

CREATE TABLE [dbo].[Producto] (
    [IdProducto] INT IDENTITY(1,1) NOT NULL,
    [IdCategoria] INT NOT NULL,
    [IdMarca] INT NOT NULL,
    [Nombre] NVARCHAR(100) NOT NULL,
    [SKU] NVARCHAR(50) NOT NULL,
    [Precio] DECIMAL(18,2) NOT NULL,
    [Estado] BIT NOT NULL,
    CONSTRAINT [PK_Producto] PRIMARY KEY ([IdProducto]),
    CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY ([IdCategoria])
        REFERENCES [dbo].[Categoria] ([IdCategoria]),
    CONSTRAINT [FK_Producto_Marca] FOREIGN KEY ([IdMarca])
        REFERENCES [dbo].[Marca] ([IdMarca])
);
GO

-- Datos exportados de CatalogoDB; se conservan los ID originales.
SET IDENTITY_INSERT [dbo].[Categoria] ON;
INSERT INTO [dbo].[Categoria] ([IdCategoria], [Nombre], [Descripcion]) VALUES
    (1, N'Laptops', N'Computadoras portatiles'),
    (2, N'Tablets Actualizadas', N'Categoria actualizada desde Postman');
SET IDENTITY_INSERT [dbo].[Categoria] OFF;
GO

SET IDENTITY_INSERT [dbo].[Marca] ON;
INSERT INTO [dbo].[Marca] ([IdMarca], [Nombre], [PaisOrigen]) VALUES
    (1, N'Samsung', N'Corea del Sur'),
    (2, N'Lenovo', N'China');
SET IDENTITY_INSERT [dbo].[Marca] OFF;
GO

SET IDENTITY_INSERT [dbo].[Producto] ON;
INSERT INTO [dbo].[Producto]
    ([IdProducto], [IdCategoria], [IdMarca], [Nombre], [SKU], [Precio], [Estado])
VALUES
    (1, 1, 2, N'Laptop Lenovo IdeaPad 3', N'LEN-LAP-001', 750.00, 1),
    (2, 2, 1, N'Samsung Galaxy A55', N'SAM-CEL-001', 450.00, 1);
SET IDENTITY_INSERT [dbo].[Producto] OFF;
GO
