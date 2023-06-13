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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace ApiWeb.Controllers
{
    [EnableCors("ReglasCors")]
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApiWebContext _context;
        private RespuestaApi _respuestaApi;

        public ClientesController(ApiWebContext context)
        {
            _context = context;
            _respuestaApi = new RespuestaApi();
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<RespuestaApi>> GetCliente()
        {
          if (_context.Cliente == null)
          {
              return NotFound();
          }
          _respuestaApi.Lclientes= await _context.Cliente.ToListAsync();
            _respuestaApi.httpStatusCode=HttpStatusCode.OK.ToString();
            return Ok(_respuestaApi);
        }

        // GET: api/Clientes/5
        [HttpGet("{cedula}")]
        public async Task<ActionResult<RespuestaApi>> GetCliente(string cedula)
        {
          if (_context.Cliente == null)
          {
              return NotFound();
          }
            _respuestaApi.Ncliente = await _context.Cliente.FindAsync(cedula);
            _respuestaApi.httpStatusCode = HttpStatusCode.OK.ToString();

            if (_respuestaApi.Ncliente == null)
            {
                _respuestaApi.httpStatusCode = HttpStatusCode.BadRequest.ToString();
                return NotFound(_respuestaApi);
            }

            return Ok(_respuestaApi);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cedula}")]
        public async Task<IActionResult> PutCliente(string cedula, Cliente cliente)
        {
            if (cedula != cliente.cedula)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cedula))
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
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
          if (_context.Cliente == null)
          {
              return Problem("Entity set 'ApiWebContext.Cliente'  is null.");
          }
            _context.Cliente.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.cedula))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCliente", new { cedula = cliente.cedula }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{cedula}")]
        public async Task<IActionResult> DeleteCliente(string cedula)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var cliente = await _context.Cliente.FindAsync(cedula);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(string cedula)
        {
            return (_context.Cliente?.Any(e => e.cedula == cedula)).GetValueOrDefault();
        }
    }
}
