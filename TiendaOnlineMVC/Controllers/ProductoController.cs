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
