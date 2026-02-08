using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_Trabajadores.Models.ViewModels
{
    public class TrabajadorDTO
    {
        public int Id { get; set; }

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string Tipo_Documento { get; set; } = null!;

        public string Nro_Documento { get; set; } = null!;

        public string Sexo { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha_Nacimiento { get; set; }

        public string? Foto { get; set; }

        public string Direccion { get; set; } = null!;

        public DateTime Fecha_Creacion { get; set; }
    }
}
