using DisneyAPI.Models;
using DisneyAPI.Repositorio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IPersonajeRepository _personajesRepository;
        private readonly IPeliculasRepository _peliculasRepository;
        public UnitTest1(IPersonajeRepository personajeRepo, IPeliculasRepository peliRepo)
        {
            _personajesRepository = personajeRepo;
            _peliculasRepository = peliRepo;



        }

        [TestMethod]
        public void TestAltaPersonaje()
        {

        }

    }
}