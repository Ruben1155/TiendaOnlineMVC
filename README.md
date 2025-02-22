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

git clone https://github.com/Ruben1155/TiendaOnlineMVC.git

---

### 2. Configuración de la Base de Datos
Usando SQL Server
Abre SQL Server Management Studio (SSMS) o la herramienta que prefieras.

Ejecuta el siguiente script para crear la base de datos y la tabla Productos:

CREATE DATABASE TiendaDB;
GO

USE TiendaDB;
GO

CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL,
    Precio DECIMAL(18, 2) NOT NULL,
    Stock INT NOT NULL
);
GO

Usando SQLite (Opcional)
Instala el paquete NuGet correspondiente (por ejemplo, Microsoft.Data.Sqlite).
Configura la cadena de conexión en appsettings.json (ver sección siguiente).
3. Configuración de la Cadena de Conexión
Modifica el archivo appsettings.json con la configuración de la conexión a la base de datos. Por ejemplo:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=RUBENE\\UNIVERSIDAD; Database=TiendaDB; User Id=sa; Password=sprt0912*; TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

4. Instalación y Configuración en Visual Studio
Abre la solución (.sln) en Visual Studio.
Restaura los paquetes NuGet (asegúrate de tener instalado el paquete Microsoft.Data.SqlClient para ADO.NET).
Compila la solución.
Configura el perfil de ejecución (por ejemplo, habilita HTTPS si es necesario).
Estructura del Proyecto y Código Fuente
appsettings.json
Archivo de configuración que contiene la cadena de conexión y ajustes de logging.

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=RUBENE\\UNIVERSIDAD; Database=TiendaDB; User Id=sa; Password=sprt0912*; TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
Program.cs
Configuración del contenedor de servicios, inyección de dependencias y configuración del pipeline HTTP.

using TiendaOnlineMVC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

// Registrar el repositorio para inyección de dependencias
builder.Services.AddScoped<ProductoRepository>();

var app = builder.Build();

// Configuración del pipeline HTTP...
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Producto}/{action=Index}/{id?}");

app.Run();
Modelo: Producto
Definición del modelo con validaciones mediante Data Annotations.

csharp
Copiar
Editar
using System.ComponentModel.DataAnnotations;

namespace TiendaOnlineMVC.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(255, ErrorMessage = "La descripción no puede exceder 255 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es requerido.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0.")]
        public int Stock { get; set; }
    }
}

Repositorio: ProductoRepository
Clase encargada de interactuar con la base de datos mediante ADO.NET.

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TiendaOnlineMVC.Models;

namespace TiendaOnlineMVC.Repositories
{
    public class ProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Método para obtener todos los productos
        public List<Producto> GetProductos()
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = "SELECT Id, Nombre, Descripcion, Precio, Stock FROM Productos";
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new Producto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Stock = Convert.ToInt32(reader["Stock"])
                        });
                    }
                }
            }
            return productos;
        }

        // Método para agregar un producto
        public void AddProducto(Producto producto)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock) VALUES (@Nombre, @Descripcion, @Precio, @Stock)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para actualizar un producto
        public void UpdateProducto(Producto producto)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = "UPDATE Productos SET Nombre=@Nombre, Descripcion=@Descripcion, Precio=@Precio, Stock=@Stock WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", producto.Id);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para eliminar un producto
        public void DeleteProducto(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = "DELETE FROM Productos WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
Controlador: ProductoController
Controlador encargado de gestionar las operaciones CRUD y redirigir a las vistas correspondientes.

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TiendaOnlineMVC.Models;
using TiendaOnlineMVC.Repositories;

namespace TiendaOnlineMVC.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoRepository _repository;

        public ProductoController(ProductoRepository repository)
        {
            _repository = repository;
        }

        // Muestra la lista de productos
        public IActionResult Index()
        {
            var productos = _repository.GetProductos();
            return View(productos);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _repository.AddProducto(producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Producto/Edit/5
        public IActionResult Edit(int id)
        {
            var producto = _repository.GetProductos().FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateProducto(producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Producto/Delete/5
        public IActionResult Delete(int id)
        {
            var producto = _repository.GetProductos().FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteProducto(id);
            return RedirectToAction("Index");
        }
    }
}
Vistas
Index.cshtml
Vista que muestra la lista de productos en una tabla con enlaces para editar y eliminar.

@model IEnumerable<TiendaOnlineMVC.Models.Producto>

<h2>Lista de Productos</h2>
<p>
    <a class="btn btn-primary" href="@Url.Action("Create")">Crear Nuevo Producto</a>
</p>
<table class="table table-bordered">
    <thead>
        <tr>
            <th>Nombre</th>
            <th>Descripción</th>
            <th>Precio</th>
            <th>Stock</th>
            <th>Acciones</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var producto in Model)
        {
            <tr>
                <td>@producto.Nombre</td>
                <td>@producto.Descripcion</td>
                <td>@producto.Precio</td>
                <td>@producto.Stock</td>
                <td>
                    <a href="@Url.Action("Edit", new { id = producto.Id })">Editar</a> |
                    <a href="@Url.Action("Delete", new { id = producto.Id })">Eliminar</a>
                </td>
            </tr>
        }
    </tbody>
</table>
Create.cshtml
Vista que muestra el formulario para crear un nuevo producto, incluyendo validación.

@model TiendaOnlineMVC.Models.Producto

<h2>Crear Producto</h2>

<form asp-action="Create" method="post">
    <div class="form-group">
        <label asp-for="Nombre"></label>
        <input asp-for="Nombre" class="form-control" />
        <span asp-validation-for="Nombre" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="Descripcion"></label>
        <input asp-for="Descripcion" class="form-control" />
        <span asp-validation-for="Descripcion" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="Precio"></label>
        <input asp-for="Precio" class="form-control" />
        <span asp-validation-for="Precio" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="Stock"></label>
        <input asp-for="Stock" class="form-control" />
        <span asp-validation-for="Stock" class="text-danger"></span>
    </div>
    <button type="submit" class="btn btn-success">Guardar</button>
</form>

@section Scripts {
    @{
        await Html.RenderPartialAsync("_ValidationScriptsPartial");
    }
}
Uso de la Aplicación
Visualizar Productos:
Accede a https://localhost:{puerto}/Producto/Index para ver la lista de productos registrados.

Crear Producto:
Navega a https://localhost:{puerto}/Producto/Create, llena el formulario y guarda. El producto se agregará a la base de datos.

Editar Producto:
Desde la vista de lista, haz clic en Editar en el producto deseado, realiza los cambios y guarda.

Eliminar Producto:
Desde la vista de lista, haz clic en Eliminar y confirma la acción para borrar el producto de la base de datos.

Contribuciones
Si deseas contribuir a este proyecto, abre un issue o envía un pull request. Todas las contribuciones serán bienvenidas.

Licencia
Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo LICENSE para más detalles.

---

Este archivo README.md está organizado en secciones que describen el proyecto, los requisitos, la configuración, la estructura del código fuente y el uso de la aplicación. Puedes modificar y ampliar cada sección según tus necesidades específicas.
