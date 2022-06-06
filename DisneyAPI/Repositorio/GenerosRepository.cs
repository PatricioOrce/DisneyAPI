using DisneyAPI.Models;

namespace DisneyAPI.Repositorio
{
    public class GenerosRepository : Repository<Genero>, IGenerosRepository
    {
        public GenerosRepository(DisneyContext ctx) : base(ctx)
        {
        }
    }
}
