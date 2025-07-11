using Actividad4LengProg3.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Actividad4LengProg3.Models
{
    public class CalificacionViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(MatriculaEstudiante))]
        public string? MatriculaEstudiante { get; set; }

        [Required]
        [ForeignKey(nameof(CodigoMateria))]
        public string? CodigoMateria { get; set; }

        [Required, Range(0, 100)]
        public int Nota { get; set; }

        [Required]
        public  string Periodo { get; set; }


    }
}

