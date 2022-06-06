using DisneyAPI.Models;

namespace DisneyAPI.Repositorio
{
    public interface IPersonajeRepository : IRepository<Personaje>
    {
        List<int> GetMovieList(PersonajeViewModel model);
    }
}
