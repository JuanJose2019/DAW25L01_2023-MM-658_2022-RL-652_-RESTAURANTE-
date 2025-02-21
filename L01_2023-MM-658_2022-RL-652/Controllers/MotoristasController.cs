using L01_2023_MM_658_2022_RL_652.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2023_MM_658_2022_RL_652.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristasController : ControllerBase
    {
        private readonly RestauranteContext _context;

        public MotoristasController(RestauranteContext context)

        {
            _context = context;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.motoristas.ToListAsync());
        }


        [HttpGet]
        [Route("GetById/{id}")]// Buscar motorista por el ID 
        public async Task<IActionResult> GetById(int id)
        {
            var motorista = await _context.motoristas.FindAsync(id);
            if (motorista == null)
            {
                return NotFound();
            }
            return Ok(motorista);
        }

        
        [HttpPost]
        [Route("Añadir")]// Agregar un nuevo motorista
        public async Task<IActionResult> Create([FromBody] motoristas motorista)
        {
            if (motorista == null)
            {
                return BadRequest();
            }

            _context.motoristas.Add(motorista);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = motorista.motoristaId }, motorista);
        }

        
        [HttpPut]
        [Route("Actualizar/{id}")]// metodo para actulizar motorista
        public async Task<IActionResult> Update(int id, [FromBody] motoristas motorista)
        {
            if (id != motorista.motoristaId)
            {
                return BadRequest();
            }

            var existingMotorista = await _context.motoristas.FindAsync(id);
            if (existingMotorista == null)
            {
                return NotFound();
            }

            existingMotorista.nombreMotorista = motorista.nombreMotorista;
            await _context.SaveChangesAsync();

            return NoContent();
        }

       
        [HttpDelete]
        [Route("Eliminar/{id}")]// metodo de eliminacion 
        public async Task<IActionResult> Delete(int id)
        {
            var motorista = await _context.motoristas.FindAsync(id);
            if (motorista == null)
            {
                return NotFound();
            }

            _context.motoristas.Remove(motorista);
            await _context.SaveChangesAsync();
            return NoContent();
        }

       
        [HttpGet]
        [Route("Buscar")]// metodo de filtrado para nombre de motorista
        public async Task<IActionResult> Search([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest();
            }

            var motoristas = await _context.motoristas
                                          .Where(m => m.nombreMotorista.Contains(nombre))
                                          .ToListAsync();

            if (!motoristas.Any())
            {
                return NotFound();
            }

            return Ok(motoristas);
        }

    }
}
