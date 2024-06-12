using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.Models;

public partial class BC100520Context :DbContext
{
    public BC100520Context()
    {
    }

    public BC100520Context(DbContextOptions<BC100520Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<CursosInscrito> CursosInscritos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    public virtual DbSet<ProfesoresCurso> ProfesoresCursos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=ANONYMOUS\\SQLEXPRESS;Database=RA102011;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__Cursos__FE00CD1C55A88D4B");

            entity.Property(e => e.IdCurso).HasColumnName("Id_curso");
            entity.Property(e => e.NombreCurso)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Curso");
        });

        modelBuilder.Entity<CursosInscrito>(entity =>
        {
            entity.HasKey(e => e.IdInscripcion).HasName("PK__Cursos_i__5023178265E09D06");

            entity.ToTable("Cursos_inscritos");

            entity.Property(e => e.IdInscripcion).HasColumnName("Id_inscripcion");
            entity.Property(e => e.IdCurso).HasColumnName("Id_curso");
            entity.Property(e => e.IdEstudiante).HasColumnName("Id_estudiante");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.CursosInscritos)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("FK__Cursos_in__Id_cu__45F365D3");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.CursosInscritos)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("FK__Cursos_in__Id_es__44FF419A");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__Estudian__BE32832D7D998CEC");

            entity.Property(e => e.IdEstudiante).HasColumnName("Id_estudiante");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaNac)
                .HasColumnType("date")
                .HasColumnName("Fecha_nac");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("PK__Profesor__9298F53ACAB32A09");

            entity.Property(e => e.IdProfesor).HasColumnName("Id_profesor");
            entity.Property(e => e.NombreProfesor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_profesor");
        });

        modelBuilder.Entity<ProfesoresCurso>(entity =>
        {
            entity.HasKey(e => e.IdProfesoresCurso).HasName("PK__Profesor__F44C1242BA72AC61");

            entity.ToTable("Profesores_Curso");

            entity.Property(e => e.IdProfesoresCurso).HasColumnName("Id_profesoresCurso");
            entity.Property(e => e.IdCurso).HasColumnName("Id_curso");
            entity.Property(e => e.IdProfesor).HasColumnName("Id_profesor");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.ProfesoresCursos)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("FK__Profesore__Id_cu__3F466844");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.ProfesoresCursos)
                .HasForeignKey(d => d.IdProfesor)
                .HasConstraintName("FK__Profesore__Id_pr__403A8C7D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2D95A894FA2DBDB9");

            entity.Property(e => e.IdRol).HasColumnName("Id_rol");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Role_name");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__EF59F76264DEDB38");

            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdRol).HasColumnName("Id_rol");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuarios__Id_rol__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
