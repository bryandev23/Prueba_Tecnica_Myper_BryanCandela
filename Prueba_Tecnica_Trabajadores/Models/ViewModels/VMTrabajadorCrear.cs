using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_Trabajadores.Models.ViewModels
{
    public class VMTrabajadorCrear
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50)]
        public string Apellidos { get; set; } = null!;


        [Required(ErrorMessage = "Debe seleccionar un tipo de documento")]
        [Display(Name = "Tipo de Documento")]
        public int DocumentoId { get; set; } 


        [Required(ErrorMessage = "El número de documento es obligatorio")]
        [Display(Name = "Número de Documento")]
        [StringLength(50)]
        public string NroDocumento { get; set; } = null!;


        [Required(ErrorMessage = "Debe seleccionar el sexo")]
        public string Sexo { get; set; } = null!;


        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Foto de Perfil")]
        public IFormFile? FotoArchivo { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200)]
        public string Direccion { get; set; } = null!;

    }
}
