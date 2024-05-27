using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataObjects;
using DataObjects.Context;

namespace CSMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CruisesController : ControllerBase
    {
        private readonly CSMDBContext _context;

        public CruisesController(CSMDBContext context)
        {
            _context = context;
        }

        // GET: api/Cruises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cruise>>> GetCruises()
        {
            return await _context.Cruises.ToListAsync();
        }

        // GET: api/Cruises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cruise>> GetCruise(int id)
        {
            var cruise = await _context.Cruises.FindAsync(id);

            if (cruise == null)
            {
                return NotFound();
            }

            return cruise;
        }

        // PUT: api/Cruises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCruise(int id, Cruise cruise)
        {
            if (id != cruise.Id)
            {
                return BadRequest();
            }

            _context.Entry(cruise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CruiseExists(id))
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

        // POST: api/Cruises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cruise>> PostCruise(Cruise cruise)
        {
            _context.Cruises.Add(cruise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCruise", new { id = cruise.Id }, cruise);
        }

        // DELETE: api/Cruises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCruise(int id)
        {
            var cruise = await _context.Cruises.FindAsync(id);
            if (cruise == null)
            {
                return NotFound();
            }

            _context.Cruises.Remove(cruise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CruiseExists(int id)
        {
            return _context.Cruises.Any(e => e.Id == id);
        }
    }
}
