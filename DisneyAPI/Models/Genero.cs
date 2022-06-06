using System.ComponentModel.DataAnnotations;

namespace DisneyAPI.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre no puede estar vacio.")]
        [StringLength(50)]
        public string Nombre { get; set; } 
        public string? Imagen { get; set; }
        public ICollection<Pelicula>? Peliculas { get; set; }

    }
}
