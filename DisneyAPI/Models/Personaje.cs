using System.ComponentModel.DataAnnotations;

namespace DisneyAPI.Models
{
    public class Personaje
    {
        [Key]
        public int Id { get; set; }
        public string? Imagen { get; set; }
        [Required(ErrorMessage = "El campo Titulo no puede estar vacio.")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Edad no puede estar vacio.")]
        public int Edad { get; set; }
        public decimal Peso { get; set; }
        public string? Historia { get; set; }
        
    }
}
