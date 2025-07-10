using Actividad4LengProg3.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Actividad4LengProg3.Models
{
    public class CalificacionViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MatriculaEstudiante { get; set; }

        [Required]
        public  string CodigoMateria { get; set; }

        [Required, Range(0, 100)]
        public int Nota { get; set; }

        [Required]
        public  string Periodo { get; set; }

        // Relaciones
        [ForeignKey("MatriculaEstudiante")]
       public required EstudianteViewModel Estudiante { get; set; }

        [ForeignKey("CodigoMateria")]
        public required MateriaViewModel Materia { get; set; }
    }
}
