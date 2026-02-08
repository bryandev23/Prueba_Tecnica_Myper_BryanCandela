using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_Trabajadores.Models.ViewModels
{
    public class VMTrabajadorEditar
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50)]
        public string Apellidos { get; set; } = null!;

        [Required]
        [Display(Name = "Tipo de Documento")]
        public int DocumentoId { get; set; }

        [Required]
        [StringLength(50)]
        public string NroDocumento { get; set; } = null!;

        [Required]
        public string Sexo { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Cambiar Foto (Opcional)")]
        public IFormFile? FotoArchivo { get; set; }

        [Required]
        [StringLength(200)]
        public string Direccion { get; set; } = null!;
    }
}
