using L01_2023_MM_658_2022_RL_652.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2023_MM_658_2022_RL_652.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly RestauranteContext _restauranteContexto;
        public pedidosController(RestauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            var listadoPedidos = (from pe in _restauranteContexto.pedidos
                                  join p in _restauranteContexto.platos
                                  on pe.platoId equals p.platoId
                                  join c in _restauranteContexto.clientes
                                  on pe.clienteId equals c.clienteId
                                  join m in _restauranteContexto.motoristas
                                  on pe.motoristaId equals m.motoristaId
                                  select new
                                  {
                                      pe.pedidoId,
                                      pe.clienteId,
                                      c.nombreCliente,
                                      pe.platoId,
                                      p.nombrePlato,
                                      pe.motoristaId,
                                      m.nombreMotorista,
                                      pe.cantidad,
                                      pe.precio
                                  });


            if (listadoPedidos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPedidos);
        }


        [HttpGet]
        [Route("GetByMotorista/{id}")]
        public IActionResult GetMotorista(int id)
        {
            pedidos? pedido = (from e in _restauranteContexto.pedidos
                               where e.motoristaId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpGet]
        [Route("GetByClient/{id}")]
        public IActionResult GetCliente(int id)
        {
            pedidos? pedido = (from e in _restauranteContexto.pedidos
                               where e.clienteId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPedido([FromBody] pedidos pedido)
        {
            try
            {
                _restauranteContexto.pedidos.Add(pedido);
                _restauranteContexto.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedidoModificar)
        {
            pedidos? pedidoActual = (from e in _restauranteContexto.pedidos where e.pedidoId == id select e).FirstOrDefault();

            if (pedidoActual == null)
            {
                return NotFound();
            }

            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.clienteId= pedidoModificar.clienteId;
            pedidoActual.motoristaId= pedidoModificar.motoristaId;
            pedidoActual.cantidad=pedidoModificar.cantidad;
            pedidoActual.precio=pedidoModificar.precio;

            _restauranteContexto.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();
            return Ok(pedidoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarPedido(int id)
        {
            pedidos? pedido = (from e in _restauranteContexto.pedidos where e.pedidoId == id select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            _restauranteContexto.pedidos.Attach(pedido);
            _restauranteContexto.pedidos.Remove(pedido);
            _restauranteContexto.SaveChanges();

            return Ok(pedido);
        }
    }
}
