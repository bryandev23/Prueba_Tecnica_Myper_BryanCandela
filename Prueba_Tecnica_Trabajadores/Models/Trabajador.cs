using System;
using System.Collections.Generic;

namespace Prueba_Tecnica_Trabajadores.Models;

public partial class Trabajador
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public int DocumentoId { get; set; }

    public string NroDocumento { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string? Foto { get; set; }

    public string Direccion { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Documento Documento { get; set; } = null!;
}
