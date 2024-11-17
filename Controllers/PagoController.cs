using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AccesoDatos.Models;

namespace AccesoDatos.Controllers
{
    [Route("api/pago")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PagoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Pago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos([FromQuery] string searchString = "")
        {
            var pagos = _context.pagos.Include(p => p.Cliente).Include(p => p.Membresia).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                pagos = pagos.Where(p => p.Cliente.Apellido.Contains(searchString) || p.Membresia.Tipo.Contains(searchString));
            }

            return await pagos.ToListAsync();
        }

        // GET: api/Pago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.pagos
                .Include(p => p.Cliente)
                .Include(p => p.Membresia)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        // POST: api/Pago
        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
        {
            _context.pagos.Add(pago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPago", new { id = pago.Id }, pago);
        }

        // PUT: api/Pago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.Id)
            {
                return BadRequest();
            }

            _context.Entry(pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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

        // DELETE: api/Pago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.pagos.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagoExists(int id)
        {
            return _context.pagos.Any(e => e.Id == id);
        }
    }
}
