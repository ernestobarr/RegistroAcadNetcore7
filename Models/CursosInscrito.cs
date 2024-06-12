using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class CursosInscrito
{
    public int IdInscripcion { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdCurso { get; set; }

    public virtual Curso? IdCursoNavigation { get; set; }

    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
