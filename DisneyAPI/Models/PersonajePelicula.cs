using Microsoft.EntityFrameworkCore;

namespace DisneyAPI.Models
{
    [Keyless]
    public class PersonajePelicula
    {
        public int PersonajeId { get; set; }
        public int PeliculaId { get; set; }
    }
}
