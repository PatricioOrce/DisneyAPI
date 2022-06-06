using DisneyAPI.Models;
using DisneyAPI.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DisneyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CharactersController : Controller
    {
        private readonly IPersonajeRepository _repository;
        public CharactersController(IPersonajeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            List<Object> list = new();
            _repository.GetAll().Result.ForEach(x =>
            {
                list.Add(new
                {
                    Imagen = x.Imagen,
                    Nombre = x.Nombre
                });

            });
            return Ok(list);
        }
        [HttpPost("newPersonaje")]
        [AllowAnonymous]
        public async Task<IActionResult> Add(PersonajeViewModel personaje)
        {
            string errorMessage = String.Empty;
            Personaje model = new Personaje
            {
                Imagen = personaje.Imagen,
                Nombre = personaje.Nombre,
                Edad = personaje.Edad,
                Historia = personaje.Historia,
                Peso = personaje.Peso,
                Id = 0
            };
            try
            {
                if(!Exist(model).Result)
                {
                    if (_repository.Create(model).Result)
                    {
                        StringBuilder sb = new();
                        sb.AppendLine("Personaje Creado con exito!");
                        sb.AppendLine($"Nombre: {personaje.Nombre}");
                        sb.AppendLine($"Edad: {personaje.Edad}");
                        return Ok(sb.ToString());
                    }
                }
                else
                {
                    return BadRequest("Personaje ya existe");
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return NotFound($"Inconveniente al crear personaje {errorMessage}");

        }

        [HttpPut("updatePersonaje")]
        public async Task<IActionResult> Edit(PersonajeViewModel personaje)
        {
            Personaje model = new Personaje
            {
                Imagen = personaje.Imagen,
                Nombre = personaje.Nombre,
                Edad = personaje.Edad,
                Historia = personaje.Historia,
                Peso = personaje.Peso,
                Id = 0
            };
            if (Exist(model).Result)
            {
                try
                {
                     return Ok(await _repository.Update(model));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound("Personaje no encontrado");
        }

        [HttpDelete("deletePersonaje/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Personaje personaje;
            try
            {
                personaje = await _repository.GetById(id);

                if (personaje is not null)
                {
                    if (Exist(personaje).Result)
                    {
                        await _repository.Delete(id);
                        return Ok("Personaje eliminado con exito.");
                    }
                }
            }
            catch (Exception ex)
            {
                 return BadRequest(ex.Message);
            }

            return NotFound("Personaje no encontrado");
        }

        [HttpGet("details/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            Personaje model = await _repository.GetById(id);
            if(model != null)
            {
                PersonajeViewModel viewModel = new PersonajeViewModel
                {
                    Imagen = model.Imagen,
                    Nombre = model.Nombre,
                    Edad = model.Edad,
                    Peso = model.Peso,
                    Historia = model.Historia
                };

                return Ok(viewModel);
            }
            return Ok(model);
        }

        private async Task<bool> Exist(Personaje model)
        {
            List<Personaje> listModel = await _repository.GetAll();

            if(model != null && listModel.Count > 0)
            {
                foreach (var item in listModel)
                {
                    if(model.Nombre.ToLower() == item.Nombre.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
