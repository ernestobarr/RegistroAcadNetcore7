using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string? Nombre { get; set; }

    public string? CorreoElectronico { get; set; }

    public DateTime? FechaNac { get; set; }

    public virtual ICollection<CursosInscrito> CursosInscritos { get; set; } = new List<CursosInscrito>();
}
