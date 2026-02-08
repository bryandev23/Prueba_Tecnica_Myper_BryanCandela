using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_Trabajadores.Services;
using Prueba_Tecnica_Trabajadores.Models.ViewModels;

namespace Prueba_Tecnica_Trabajadores.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly ITrabajadorService _service;

        public TrabajadorController(ITrabajadorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string busqueda = "", string sexo = "")
        {
            var lista = await _service.ObtenerListado();

            if (!string.IsNullOrEmpty(busqueda))
            {
                busqueda = busqueda.ToLower();
                lista = lista.Where(t =>
                    t.Nombres.ToLower().Contains(busqueda) ||
                    t.Apellidos.ToLower().Contains(busqueda) ||
                    t.Nro_Documento.Contains(busqueda)
                ).ToList();
            }

            if (!string.IsNullOrEmpty(sexo))
            {
                lista = lista.Where(t => t.Sexo == sexo).ToList();
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_TablaTrabajadores", lista);
            }

            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Documentos = await _service.ObtenerTiposDocumento();
            return PartialView("_Crear", new VMTrabajadorCrear());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(VMTrabajadorCrear modelo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Documentos = await _service.ObtenerTiposDocumento();
                return Json(new { success = false, message = "Datos incompletos" });
            }

            bool resultado = await _service.CrearTrabajador(modelo);

            if (resultado)
                return Json(new { success = true, message = "Trabajador registrado correctamente" });
            else
                return Json(new { success = false, message = "Error al guardar (posible duplicado)" });
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var modelo = await _service.ObtenerTrabajadorParaEditar(id);
            if (modelo == null) return NotFound();

            ViewBag.Documentos = await _service.ObtenerTiposDocumento();
            return PartialView("_Editar", modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(VMTrabajadorEditar modelo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Documentos = await _service.ObtenerTiposDocumento();
                return Json(new { success = false, message = "Datos inválidos" });
            }

            bool resultado = await _service.EditarTrabajador(modelo);

            if (resultado)
                return Json(new { success = true, message = "Actualizado correctamente" });
            else
                return Json(new { success = false, message = "Error al actualizar" });
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool resultado = await _service.EliminarTrabajador(id);

            if (resultado)
                return Json(new { success = true, message = "Eliminado correctamente" });
            else
                return Json(new { success = false, message = "Error al eliminar" });
        }
    }
}
