# TiendaOnlineMVC

Este proyecto es una aplicación web desarrollada con ASP.NET Core MVC que permite gestionar un catálogo de productos para una tienda en línea. La aplicación implementa operaciones CRUD (Crear, Leer, Actualizar y Eliminar) sobre productos utilizando ADO.NET para interactuar con una base de datos SQL Server (o SQLite). La configuración se realiza a través del archivo `appsettings.json` y se aprovecha la inyección de dependencias para organizar el acceso a datos.

---

## Tabla de Contenidos

- [Características](#características)
- [Requisitos](#requisitos)
- [Configuración del Proyecto](#configuración-del-proyecto)
  - [1. Clonar el Repositorio](#1-clonar-el-repositorio)
  - [2. Configuración de la Base de Datos](#2-configuración-de-la-base-de-datos)
  - [3. Configuración de la Cadena de Conexión](#3-configuración-de-la-cadena-de-conexión)
  - [4. Instalación y Configuración en Visual Studio](#4-instalación-y-configuración-en-visual-studio)
- [Estructura del Proyecto y Código Fuente](#estructura-del-proyecto-y-código-fuente)
  - [appsettings.json](#appsettingsjson)
  - [Program.cs](#programcs)
  - [Modelo: Producto](#modelo-producto)
  - [Repositorio: ProductoRepository](#repositorio-productorepository)
  - [Controlador: ProductoController](#controlador-productocontroller)
  - [Vistas](#vistas)
    - [Index.cshtml](#indexcshtml)
    - [Create.cshtml (Crear Producto)](#createcshtml-crear-producto)
    - [Edit.cshtml (Editar Producto)](#Editcshtml-editar-producto)
    - [Delete.cshtml (Eliminar Producto)](#deletecshtml-eliminar-producto)
- [Uso de la Aplicación](#uso-de-la-aplicación)
- [Contribuciones](#contribuciones)
- [Licencia](#licencia)

---

## Características

- **Operaciones CRUD:** Permite crear, leer, actualizar y eliminar productos.
- **Validación de Datos:** Uso de Data Annotations para asegurar que los campos cumplan con los requisitos (por ejemplo, precio mayor que 0 y stock no negativo).
- **Acceso a Datos con ADO.NET:** Se utilizan clases como `SqlConnection`, `SqlCommand` y `SqlDataReader` para la interacción directa con la base de datos.
- **Configuración Moderna:** La cadena de conexión se define en `appsettings.json` en lugar de usar `web.config`.
- **Inyección de Dependencias:** El repositorio se registra en el contenedor de servicios para un código más limpio y modular.

---

## Requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download) o superior.
- SQL Server Express o una instancia de SQL Server (o SQLite si se prefiere).
- Visual Studio 2022 (o Visual Studio Code) con soporte para ASP.NET Core.
- Git para clonar y gestionar el repositorio.

---

## Configuración del Proyecto

### 1. Clonar el Repositorio

Clona el repositorio en tu máquina local:

```bash
git clone https://github.com/Ruben1155/TiendaOnlineMVC.git


---

Este archivo README.md está organizado en secciones que describen el proyecto, los requisitos, la configuración, la estructura del código fuente y el uso de la aplicación. Puedes modificar y ampliar cada sección según tus necesidades específicas.
