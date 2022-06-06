using DisneyAPI.Models;

namespace DisneyAPI.Repositorio
{
    public class PersonajesRepository : Repository<Personaje>, IPersonajeRepository
    {
        DisneyContext context;
        public PersonajesRepository(DisneyContext ctx) : base(ctx)
        {
            this.context = ctx;
        }

        //public List<int> GetMoviesByCharId(int charId)
        //{
        //    List<int> ids = new();
        //    List<PersonajePelicula> model = context.
        //}
        public List<int> GetMovieList(PersonajeViewModel model)
        {
            List<Pelicula> peliculas = new();
            List<Pelicula> returnList = new();
            if(model != null)
            {
                peliculas = context.Peliculas.ToList();
                foreach (var pelicula in peliculas)
                {
                    foreach (var personajeId in model.PeliculasId)
                    {
                        if(pelicula.Id == personajeId)
                        {
                            returnList.Add(pelicula);
                            context.PersonajePelicula.Add(new PersonajePelicula
                            {
                                PersonajeId = personajeId,
                                PeliculaId = pelicula.Id
                            });
                        }
                    }
                }
            }
            context.SaveChanges();
            return null;
        }

    }
}
