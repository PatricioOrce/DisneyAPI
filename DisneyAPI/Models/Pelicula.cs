using System.ComponentModel.DataAnnotations;

namespace DisneyAPI.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }
        public string? Imagen { get; set; }
        [Required(ErrorMessage = "El campo Titulo no puede estar vacio.")]
        [StringLength(50)]
        public string Titulo { get; set; }
        public Genero Genero { get; set; }
        [Required(ErrorMessage = "El campo Fecha no puede estar vacio.")]
        public DateTime FechaCreacion { get; set; }
        public int Calificacion { get; set; }

    }
}
