using DisneyAPI.Models;
using DisneyAPI.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace DisneyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PeliculasController : Controller
    {
        private readonly IPeliculasRepository _repository;
        public PeliculasController(IPeliculasRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Pelicula model;
            try
            {
                model = await _repository.GetById(id);
                if (model is not null)
                {
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Hubo un problema al recolectar la informacion solicitada: {ex.Message}");          
            }
            return NotFound();
        }
        [HttpGet("listado")]
        public async Task<IActionResult> GetAll()
        {
            List<Pelicula> listModel;
            listModel = await _repository.GetAll();
            if(listModel.Count > 0)
            {
                return Ok(listModel);
            }
            return NotFound();
        }
        [HttpPost("newPelicula")]
        public async Task<IActionResult> Create(Pelicula model)
        {
            if(model is not null)
            {
                if(_repository.Create(model).Result)
                {
                    return Ok(model);
                }
            }
            return BadRequest("Pelicula Invalida");
        }
        [HttpPut("updatePelicula")]
        public async Task<IActionResult> Edit(Pelicula pelicula)
        {
            List<Pelicula> peliculaList;
            if (pelicula is not null)
            {
                peliculaList = await _repository.Update(pelicula);
                return Ok(peliculaList);
            }
            return NotFound("Pelicula no encontrada");

        }
        [HttpDelete("deletePersonaje/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Pelicula model;
            model = await _repository.GetById(id);
            if (model is not null)
            {
                //Utilizar try catch....
                _repository.Delete(id);
                return Ok("Pelicula eliminada con exito.");
            }
            return NotFound("Pelicula no encontrada");
        }
    }
}
