using Actividad4LengProg3.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Actividad4LengProg3.Data
{
    public class EstudiantesDbContext: DbContext
    {
        public EstudiantesDbContext (DbContextOptions<EstudiantesDbContext> options) : base(options) { }

        public DbSet<EstudianteViewModel> Estudiantes { get; set; }
        public DbSet<MateriaViewModel> Materias { get; set; }
        public DbSet<CalificacionViewModel> Calificaciones { get; set; }
    }
}
