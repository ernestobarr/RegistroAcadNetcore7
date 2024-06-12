using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class Role 
{
    public int IdRol { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
