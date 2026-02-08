using Prueba_Tecnica_Trabajadores.Models;
using Prueba_Tecnica_Trabajadores.Models.ViewModels;

namespace Prueba_Tecnica_Trabajadores.Repositories
{
    public interface ITrabajadorRepository
    {
        Task<List<TrabajadorDTO>> ObtenerTodos();
        Task<List<Documento>> ObtenerDocumentos();
        Task<bool> Insertar(Trabajador trabajador);
        Task<Trabajador?> ObtenerPorId(int id);
        Task<bool> Editar(Trabajador trabajador);
        Task<bool> Eliminar(Trabajador trabajador);
    }
}
