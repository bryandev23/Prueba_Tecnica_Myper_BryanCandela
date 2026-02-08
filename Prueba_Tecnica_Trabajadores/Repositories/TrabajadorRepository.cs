using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_Trabajadores.Models;
using Prueba_Tecnica_Trabajadores.Models.ViewModels;

namespace Prueba_Tecnica_Trabajadores.Repositories
{
    public class TrabajadorRepository : ITrabajadorRepository
    {
        private readonly TrabajadoresPruebaContext _context;

        public TrabajadorRepository(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        public async Task<List<TrabajadorDTO>> ObtenerTodos()
        {
            return await _context.Database
                .SqlQueryRaw<TrabajadorDTO>("EXEC sp_listar_trabajadores")
                .ToListAsync();
        }

        public async Task<List<Documento>> ObtenerDocumentos()
        {
            return await _context.Documentos.Where(d => d.Estado == true).ToListAsync();
        }

        public async Task<bool> Insertar(Trabajador trabajador)
        {
            try
            {
                _context.Trabajadores.Add(trabajador);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Trabajador?> ObtenerPorId(int id)
        {
            return await _context.Trabajadores.FindAsync(id);
        }

        public async Task<bool> Editar(Trabajador trabajador)
        {
            try
            {
                _context.Trabajadores.Update(trabajador);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Eliminar(Trabajador trabajador)
        {
            try
            {
                _context.Trabajadores.Remove(trabajador);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
