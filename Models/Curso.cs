using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Curso
{
    public int IdCurso { get; set; }

    public string? NombreCurso { get; set; }

    public virtual ICollection<CursosInscrito> CursosInscritos { get; set; } = new List<CursosInscrito>();

    public virtual ICollection<ProfesoresCurso> ProfesoresCursos { get; set; } = new List<ProfesoresCurso>();
}
