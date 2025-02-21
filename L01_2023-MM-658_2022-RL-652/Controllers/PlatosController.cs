using L01_2023_MM_658_2022_RL_652.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2023_MM_658_2022_RL_652.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private readonly RestauranteContext _context;

        public PlatosController(RestauranteContext context)

        {
            _context = context;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<platos>>> GetPlatos()

        {
            return await _context.platos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<platos>> GetPlato(int id)
        {
            var plato = await _context.platos.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            return plato;
        }


        [HttpPost]
        public async Task<ActionResult<platos>> PostPlatos(platos platos)
        {
            _context.platos.Add(platos);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPlato), new { id = platos.platoId }, platos);
        
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlato(int id, platos plato)
        {
            if (id != plato.platoId)
            {
                return BadRequest();
            }

            _context.Entry(plato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlato(int id)
        {
            var plato = await _context.platos.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }

            _context.platos.Remove(plato);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<platos>>> GetPlatosFiltrados([FromQuery] decimal precioMaximo)
        {
            return await _context.platos.Where(p => p.precio < precioMaximo).ToListAsync();
        }

        private bool PlatoExists(int id)
        {
            return _context.platos.Any(e => e.platoId == id);
        }
    }

}
