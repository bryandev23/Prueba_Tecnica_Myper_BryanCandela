using Prueba_Tecnica_Trabajadores.Models;
using Prueba_Tecnica_Trabajadores.Models.ViewModels;
namespace Prueba_Tecnica_Trabajadores.Services
{
    public interface ITrabajadorService
    {
        Task<List<TrabajadorDTO>> ObtenerListado();
        Task<List<Documento>> ObtenerTiposDocumento();
        Task<bool> CrearTrabajador(VMTrabajadorCrear modelo);
        Task<VMTrabajadorEditar?> ObtenerTrabajadorParaEditar(int id);
        Task<bool> EditarTrabajador(VMTrabajadorEditar modelo);
        Task<bool> EliminarTrabajador(int id);
    }
}
