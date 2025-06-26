using System.ComponentModel.DataAnnotations;

namespace Actividad4LengProg3.Models
{
    public class MateriaViewModel
    {
        [Key]
        [Required]
        public required string Codigo { get; set; }

        [Required, StringLength(100)]
        public required string Nombre { get; set; }

        [Required, Range(1, 10)]
        public int Creditos { get; set; }

        [Required]
        public required string Carrera { get; set; }
    }
}
