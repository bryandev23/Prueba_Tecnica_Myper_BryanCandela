using Prueba_Tecnica_Trabajadores.Models;
using Prueba_Tecnica_Trabajadores.Repositories;
using Prueba_Tecnica_Trabajadores.Models.ViewModels;

namespace Prueba_Tecnica_Trabajadores.Services
{
    public class TrabajadorService : ITrabajadorService
    {
        private readonly ITrabajadorRepository _repo;
        private readonly IWebHostEnvironment _env;

        public TrabajadorService(ITrabajadorRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<List<TrabajadorDTO>> ObtenerListado()
        {
            return await _repo.ObtenerTodos();
        }

        public async Task<List<Documento>> ObtenerTiposDocumento()
        {
            return await _repo.ObtenerDocumentos();
        }

        public async Task<bool> CrearTrabajador(VMTrabajadorCrear modelo)
        {
            string? rutaFoto = null;

            if (modelo.FotoArchivo != null)
            {
                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(modelo.FotoArchivo.FileName);
                string carpetaImagenes = Path.Combine(_env.WebRootPath, "imagenes");

                if (!Directory.Exists(carpetaImagenes))
                    Directory.CreateDirectory(carpetaImagenes);

                string rutaCompleta = Path.Combine(carpetaImagenes, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await modelo.FotoArchivo.CopyToAsync(stream);
                }

                rutaFoto = "/imagenes/" + nombreArchivo;
            }

            var trabajadorEntidad = new Trabajador
            {
                Nombres = modelo.Nombres,
                Apellidos = modelo.Apellidos,
                DocumentoId = modelo.DocumentoId,
                NroDocumento = modelo.NroDocumento,
                Sexo = modelo.Sexo,
                FechaNacimiento = DateOnly.FromDateTime(modelo.FechaNacimiento),
                Direccion = modelo.Direccion,
                Foto = rutaFoto 
            };

            return await _repo.Insertar(trabajadorEntidad);
        }

        public async Task<VMTrabajadorEditar?> ObtenerTrabajadorParaEditar(int id)
        {
            var trabajador = await _repo.ObtenerPorId(id);
            if (trabajador == null) return null;

            return new VMTrabajadorEditar
            {
                Id = trabajador.Id,
                Nombres = trabajador.Nombres,
                Apellidos = trabajador.Apellidos,
                DocumentoId = trabajador.DocumentoId,
                NroDocumento = trabajador.NroDocumento,
                Sexo = trabajador.Sexo,
                FechaNacimiento = trabajador.FechaNacimiento.ToDateTime(TimeOnly.MinValue),
                Direccion = trabajador.Direccion
            };
        }

        public async Task<bool> EditarTrabajador(VMTrabajadorEditar modelo)
        {
            var trabajadorDb = await _repo.ObtenerPorId(modelo.Id);
            if (trabajadorDb == null) return false;

            if (modelo.FotoArchivo != null)
            {
                if (!string.IsNullOrEmpty(trabajadorDb.Foto))
                {
                    string rutaAntigua = Path.Combine(_env.WebRootPath, trabajadorDb.Foto.TrimStart('/'));
                    if (File.Exists(rutaAntigua))
                    {
                        File.Delete(rutaAntigua);
                    }
                }

                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(modelo.FotoArchivo.FileName);
                string rutaCompleta = Path.Combine(_env.WebRootPath, "imagenes", nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await modelo.FotoArchivo.CopyToAsync(stream);
                }

                trabajadorDb.Foto = "/imagenes/" + nombreArchivo;
            }

            trabajadorDb.Nombres = modelo.Nombres;
            trabajadorDb.Apellidos = modelo.Apellidos;
            trabajadorDb.DocumentoId = modelo.DocumentoId;
            trabajadorDb.NroDocumento = modelo.NroDocumento;
            trabajadorDb.Sexo = modelo.Sexo;
            trabajadorDb.FechaNacimiento = DateOnly.FromDateTime(modelo.FechaNacimiento);
            trabajadorDb.Direccion = modelo.Direccion;
            trabajadorDb.FechaModificacion = DateTime.Now;

            return await _repo.Editar(trabajadorDb);
        }

        public async Task<bool> EliminarTrabajador(int id)
        {
            var trabajadorDb = await _repo.ObtenerPorId(id);
            if (trabajadorDb == null) return false;

            if (!string.IsNullOrEmpty(trabajadorDb.Foto))
            {
                string rutaFoto = Path.Combine(_env.WebRootPath, trabajadorDb.Foto.TrimStart('/'));
                if (File.Exists(rutaFoto))
                {
                    File.Delete(rutaFoto);
                }
            }

            return await _repo.Eliminar(trabajadorDb);
        }
    }
}
