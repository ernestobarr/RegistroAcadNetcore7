using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class ProfesoresCurso
{
    public int IdProfesoresCurso { get; set; }

    public int? IdCurso { get; set; }

    public int? IdProfesor { get; set; }

    public virtual Curso? IdCursoNavigation { get; set; }

    public virtual Profesore? IdProfesorNavigation { get; set; }
}
