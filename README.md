
# AD01 - Comercio Multibodega

## Descripción

Proyecto de comercio multibodega desarrollado mediante tres APIs REST independientes para gestionar el catálogo de productos, el inventario en bodegas y los pedidos de los clientes.

## Objetivo

Administrar la información de productos, bodegas y pedidos mediante aplicaciones distribuidas y bases de datos independientes en SQL Server.

## Tecnologías utilizadas

- C#
- ASP.NET Core Web API
- Microsoft SQL Server
- SQL (Transact-SQL)
- Visual Studio
- Swagger
- Postman
- Git y GitHub

## Bases de datos

El proyecto utiliza tres bases de datos independientes.

### 1. CatalogoDB

**Tablas:**

- Categoria: almacena las categorías.
- Marca: almacena las marcas y sus países de origen.
- Producto: almacena productos, precios, SKU y estado.

**Relaciones:**

- Producto → Categoria.
- Producto → Marca.

### 2. Inventaria_DB

**Tablas:**

- Bodega: registra las bodegas.
- Existencia: almacena el stock por bodega.
- MovimientoInventario: registra movimientos de inventario.
- ReservaStock: registra reservas de productos.

**Relaciones:**

- Existencia → Bodega.
- MovimientoInventario → Bodega.
- ReservaStock → Bodega.

Los productos se relacionan lógicamente con CatalogoDB.

### 3. PedidosDB

**Tablas:**

- Cliente: almacena información de clientes.
- Pedido: registra los pedidos.
- DetallePedido: registra los productos y cantidades de cada pedido.

**Relaciones:**

- Pedido → Cliente.
- DetallePedido → Pedido.

Los productos se relacionan lógicamente con CatalogoDB.

## Microservicios

- Catalogo.Api: gestión de categorías, marcas y productos.
- Inventario.Api: gestión de bodegas, existencias, movimientos y reservas.
- Pedidos.Api: gestión de clientes, pedidos y detalles.

## Estructura del proyecto

AD01_Entrega_Comercio_Multibodega/
│
├── README.md
│
├── Catalogo.Api/
│
├── Inventario.Api/
│
├── Pedidos.Api/
│
├── Base_de_Datos/
│   ├── CatalogoDB.sql
│   ├── Inventaria_DB.sql
│   └── PedidosDB.sql
│
└── Postman/
    └── AD01 - Microservicios.postman_collection.json

## Requisitos

- Visual Studio con soporte para ASP.NET Core.
- SQL Server o SQL Server LocalDB.
- SQL Server Management Studio (SSMS).
- Postman.

## Instalación y ejecución

1. Abrir SQL Server Management Studio.
2. Conectarse a SQL Server.
3. Ejecutar los tres scripts de Base_de_Datos en una instancia donde no existan esas bases.
4. Verificar la creación de las tablas y relaciones.
5. Abrir los proyectos .slnx en Visual Studio.
6. Configurar las conexiones en appsettings.json.
7. Ejecutar los tres microservicios.
8. Comprobar el funcionamiento mediante Swagger y Postman.

## Direcciones de las APIs

- Catalogo.Api: https://localhost:7257
- Inventario.Api: https://localhost:7183
- Pedidos.Api: https://localhost:7001

Estas direcciones corresponden a las pruebas locales y pueden variar.

## Pruebas con Postman

Se realizaron pruebas de las operaciones:

- GET
- POST
- PUT
- DELETE

También se comprobaron respuestas HTTP:

- 200 OK
- 204 No Content
- 400 Bad Request
- 404 Not Found

La colección exportada se encuentra en la carpeta Postman.

## Autor

Santiago Nicolas Togan Caicedo

## Estado del proyecto

Desarrollado y probado localmente. En preparación para su entrega en GitHub.
