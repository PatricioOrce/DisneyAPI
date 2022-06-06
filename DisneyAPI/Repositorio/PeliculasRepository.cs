using DisneyAPI.Models;

namespace DisneyAPI.Repositorio
{
    public class PeliculasRepository : Repository<Pelicula>, IPeliculasRepository
    {
        public PeliculasRepository(DisneyContext ctx) : base(ctx)
        {

        }
    }
}
