using L01_2023_MM_658_2022_RL_652.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace L01_2023_MM_658_2022_RL_652.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosTOPController : ControllerBase
    {
        private readonly RestauranteContext _restauranteContexto;
        public PlatosTOPController(RestauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetTop/{top}")]
        public IActionResult Get(int top)
        {
            var topPlatos = (from p in _restauranteContexto.platos
                             join pe in _restauranteContexto.pedidos
                             on p.platoId equals pe.platoId
                             group pe by p.nombrePlato into pedidosTop
                             select new
                             {
                                 NombrePlato = pedidosTop.Key,
                                 PedidosTotales = pedidosTop.Count()
                             }).OrderByDescending(resultado => resultado.PedidosTotales).Take(top);

            if (topPlatos == null)
            {
                return NotFound();
            }
            return Ok(topPlatos);
        }
    }
}
