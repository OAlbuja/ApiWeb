using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiWeb.Data;
using ApiWeb.Modelos;
using ApiWeb.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace ApiWeb.Controllers
{
    [EnableCors("ReglasCors")]
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductoesController : ControllerBase
    {
        private readonly ApiWebContext _context;
        private RespuestaApi _respuestaApi;

        public ProductoesController(ApiWebContext context)
        {
            _context = context;
            _respuestaApi = new RespuestaApi();
        }

        // GET: api/Productoes
        [HttpGet]
        public async Task<ActionResult<RespuestaApi>> GetProducto()
        {
          if (_context.Producto == null)
          {
              return NotFound();
          }
            _respuestaApi.Lproductos = await _context.Producto.ToListAsync();
            _respuestaApi.httpStatusCode = HttpStatusCode.OK.ToString();
            return Ok(_respuestaApi);
        }

        // GET: api/Productoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
          if (_context.Producto == null)
          {
              return NotFound();
          }
            _respuestaApi.Nproducto = await _context.Producto.FindAsync(id);
            _respuestaApi.httpStatusCode = HttpStatusCode.OK.ToString();

            if (_respuestaApi.Nproducto == null)
            {
                _respuestaApi.httpStatusCode = HttpStatusCode.BadRequest.ToString();
                return NotFound(_respuestaApi);
            }
            return Ok(_respuestaApi);
        }

        // PUT: api/Productoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Productoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
          if (_context.Producto == null)
          {
              return Problem("Entity set 'ApiWebContext.Producto'  is null.");
          }
            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
        }

        // DELETE: api/Productoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            if (_context.Producto == null)
            {
                return NotFound();
            }
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return (_context.Producto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}