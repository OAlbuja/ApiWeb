using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiWeb.Data;
using ApiWeb.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using ApiWeb.Models;
using System.Net;

namespace ApiWeb.Controllers
{
    [EnableCors("ReglasCors")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly ApiWebContext _context;
        private RespuestaApi _respuestaApi;

        public FacturasController(ApiWebContext context)
        {
            _context = context;
            _respuestaApi = new RespuestaApi();
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<RespuestaApi>> GetFactura()
        {
            if (_context.Factura == null)
            {
                return NotFound();
            }
            _respuestaApi.Lfacturas = await _context.Factura.ToListAsync();
            _respuestaApi.httpStatusCode = HttpStatusCode.OK.ToString();
            return Ok(_respuestaApi);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RespuestaApi>> GetFactura(int id)
        {
            if (_context.Factura == null)
            {
                return NotFound();
            }
            _respuestaApi.Nfactura = await _context.Factura.FindAsync(id);
            _respuestaApi.httpStatusCode = HttpStatusCode.OK.ToString();

            if (_respuestaApi.Nfactura == null)
            {
                _respuestaApi.httpStatusCode = HttpStatusCode.BadRequest.ToString();
                return NotFound(_respuestaApi);
            }

            return Ok(_respuestaApi);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, Factura factura)
        {
            if (id != factura.Id)
            {
                return BadRequest();
            }

            _context.Entry(factura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            if (_context.Factura == null)
            {
                return Problem("Entity set 'ApiWebContext.Cliente'  is null.");
            }
            _context.Factura.Add(factura);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FacturaExists(factura.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFactura", new { Id = factura.Id }, factura);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            if (_context.Factura == null)
            {
                return NotFound();
            }
            var fact = await _context.Factura.FindAsync(id);
            if (fact == null)
            {
                return NotFound();
            }

            _context.Factura.Remove(fact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacturaExists(int id)
        {
            return (_context.Factura?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
