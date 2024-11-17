using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;

namespace AccesoDatos.Controllers
{
    [Route("api/membresia")]
    [ApiController]
    public class MembresiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MembresiaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Membresia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Membresia>>> GetMembresias([FromQuery] string? searchString = null)
        {
            var membresias = _context.Membresia.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                membresias = membresias.Where(s => s.Tipo.Contains(searchString) || s.Descripcion.Contains(searchString));
            }

            var result = await membresias.ToListAsync();

            if (!result.Any())
            {
                return NotFound(new { Message = "No se encontraron membresías que coincidan con el criterio de búsqueda." });
            }

            return Ok(result);
        }

        // GET: api/Membresia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Membresia>> GetMembresia(int id)
        {
            var membresia = await _context.Membresia.FindAsync(id);

            if (membresia == null)
            {
                return NotFound(new { Message = "Membresía no encontrada." });
            }

            return Ok(membresia);
        }

        // POST: api/Membresia
        [HttpPost]
        public async Task<ActionResult<Membresia>> PostMembresia(Membresia membresia)
        {
            _context.Membresia.Add(membresia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMembresia), new { id = membresia.Id }, membresia);
        }

        // PUT: api/Membresia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMembresia(int id, Membresia membresia)
        {
            if (id != membresia.Id)
            {
                return BadRequest(new { Message = "El ID de la membresía no coincide." });
            }

            _context.Entry(membresia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembresiaExists(id))
                {
                    return NotFound(new { Message = "Membresía no encontrada." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Membresia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembresia(int id)
        {
            var membresia = await _context.Membresia.FindAsync(id);
            if (membresia == null)
            {
                return NotFound(new { Message = "Membresía no encontrada." });
            }

            _context.Membresia.Remove(membresia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MembresiaExists(int id)
        {
            return _context.Membresia.Any(e => e.Id == id);
        }
    }
}
