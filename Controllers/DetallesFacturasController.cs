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
    public class DetallesFacturasController : ControllerBase
    {
        private readonly ApiWebContext _context;
        private RespuestaApi _respuestaApi;

        public DetallesFacturasController(ApiWebContext context)
        {
            _context = context;
            _respuestaApi = new RespuestaApi();
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<RespuestaApi>> GetDetallesFacturas()
        {
            if (_context.DetallesFactura == null)
            {
                return NotFound();
            }
            _respuestaApi.Ldetallefact = await _context.DetallesFactura.ToListAsync();
            _respuestaApi.httpStatusCode = HttpStatusCode.OK.ToString();
            return Ok(_respuestaApi);
        }

        // GET: api/Clientes/5
        [HttpGet("{idFactura}")]
        public async Task<ActionResult<RespuestaApi>> GetDetallesFactura(int id)
        {
            if (_context.DetallesFactura == null)
            {
                return NotFound();
            }
            _respuestaApi.Ldetallefact = await _context.DetallesFactura.ToListAsync();
            _respuestaApi.httpStatusCode = HttpStatusCode.OK.ToString();
            
            if (_respuestaApi.Ldetallefact == null)
            {
                _respuestaApi.httpStatusCode = HttpStatusCode.BadRequest.ToString();
                return NotFound(_respuestaApi);
            }
            else
            {
                foreach (var item in _respuestaApi.Ldetallefact)
                {
                    if(item.IdFactura!=id)
                    {
                        _respuestaApi.Ldetallefact.Remove(item);
                    }
                }
                if (_respuestaApi.Ldetallefact == null)
                {
                    _respuestaApi.httpStatusCode = HttpStatusCode.BadRequest.ToString();
                    return NotFound(_respuestaApi);
                }
            }

            return Ok(_respuestaApi);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallesFactura(int id, DetallesFactura df)
        {
            if (id != df.Id)
            {
                return BadRequest();
            }

            _context.Entry(df).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallesFacturaExists(id))
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
        public async Task<ActionResult<DetallesFactura>> PostDetallesFactura(DetallesFactura df)
        {
            if (_context.DetallesFactura == null)
            {
                return Problem("Entity set 'ApiWebContext.Cliente'  is null.");
            }
            _context.DetallesFactura.Add(df);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetallesFacturaExists(df.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetallesFactura", new { Id = df.Id }, df);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallesFactura(int id)
        {
            if (_context.DetallesFactura == null)
            {
                return NotFound();
            }
            var fact = await _context.DetallesFactura.FindAsync(id);
            if (fact == null)
            {
                return NotFound();
            }

            _context.DetallesFactura.Remove(fact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallesFacturaExists(int id)
        {
            return (_context.DetallesFactura?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
