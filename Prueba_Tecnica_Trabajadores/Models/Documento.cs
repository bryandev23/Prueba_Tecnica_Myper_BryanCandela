using System;
using System.Collections.Generic;

namespace Prueba_Tecnica_Trabajadores.Models;

public partial class Documento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Trabajador> Trabajadores { get; set; } = new List<Trabajador>();
}
