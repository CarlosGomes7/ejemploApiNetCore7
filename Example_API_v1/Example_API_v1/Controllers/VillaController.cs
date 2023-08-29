using AutoMapper;
using Example_API_v1.Datos;
using Example_API_v1.Models;
using Example_API_v1.Models.DTO;
using Example_API_v1.Repositorio.IRepositorio;
using Example_API_v1.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example_API_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        //private readonly ApplicationDbContext _db;
        private readonly IVillaRepositorio _villaRepo;
        private readonly IMapper _mapper;
        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villaRepo, IMapper mapper)
        {
                _logger = logger;
                _villaRepo = villaRepo;
                _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<villaDto>>> Getvillas()
        {
            _logger.LogInformation("Obtener las villas");

            IEnumerable<Villa> villaList = await _villaRepo.GetAll();
            return Ok(villaList);
        }

        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<villaDto>> Getvilla(int id)
        {
            var response = await _villaRepo.Get(x => x.Id == id);

            if (id == 0)
            {
                return BadRequest();
            }
            if (response == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<villaDto>(response));
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<villaDto>> crearVilla([FromBody] villaCreateDto villaCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await _villaRepo.Get(v => v.Nombre.ToLower() == villaCreateDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "la villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (villaCreateDto == null)
            {
                return BadRequest();
            }
            if (villaCreateDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            
            Villa modelo = _mapper.Map<Villa>(villaCreateDto);

            await _villaRepo.Create(modelo);

            return CreatedAtRoute("GetVilla", new { id = villaCreateDto.Id }, villaCreateDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _villaRepo.Get(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            //villaStore.villaList.Remove(villa);
            await _villaRepo.delete(villa);
            
            // Si la eliminación es exitosa
            return NoContent(); // Retorna código 204 No Content

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVilla([FromBody] villaUpdateDto villaUpdateDto,int id)
        {
            if (villaUpdateDto == null || id != villaUpdateDto.Id)
            {
                return BadRequest();
            }
            var villa = await _villaRepo.Get(v => v.Id == id);
            //villa.Nombre = villaDto.Nombre;
            //villa.Ocupantes = villaDto.Ocupantes;
            //villa.MetrosCuadrados =villaDto.MetrosCuadrados;                        
 
            if (villa == null)
            {
                return NotFound();
            }           

            Villa modelo = _mapper.Map<Villa>(villaUpdateDto);

            await _villaRepo.Actualizar(modelo);

            return NoContent();
        }


        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<villaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _villaRepo.Get(v=> v.Id == id, tracked:false);

            villaUpdateDto villadto = _mapper.Map<villaUpdateDto>(villa);
           

            patchDto.ApplyTo(villadto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Villa modelo = _mapper.Map<Villa>(villadto);
           
           
            await _villaRepo.Actualizar(modelo);
            return NoContent();
        }
    }
}
