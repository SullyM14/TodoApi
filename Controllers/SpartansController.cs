using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpartansController : ControllerBase
    {
        private readonly SpartansContext _context;

        public SpartansController(SpartansContext context)
        {
            _context = context;
        }

        // GET: api/Spartans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpartansDTO>>> GetSpartans()
        {
            return await _context.Spartans.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/Spartans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpartansDTO>> GetSpartans(long id)
        {
            var spartans = await _context.Spartans.FindAsync(id);

            if (spartans == null)
            {
                return NotFound();
            }

            return ItemToDTO(spartans);
        }

        // PUT: api/Spartans/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpartans(long id, SpartansDTO spartansDTO)
        {
            if (id != spartansDTO.Id)
            {
                return BadRequest();
            }

            var spartans = await _context.Spartans.FindAsync(id);
            if (spartans == null)
            {
                return NotFound();
            }

            spartans.Name = spartansDTO.Name;
            spartans.Course = spartansDTO.Course;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!SpartansExists(id))
            {
                    return NotFound();
            }

            return NoContent();
        }

        // POST: api/Spartans
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SpartansDTO>> PostSpartans(SpartansDTO spartansDTO)
        {
            var spartans = new Spartans
            {
                Name = spartansDTO.Name,
                Course = spartansDTO.Course
            };
                
            _context.Spartans.Add(spartans);
            await _context.SaveChangesAsync();

           // return CreatedAtAction("GetSpartans", new { id = spartans.Id }, spartans);
            return CreatedAtAction(nameof(GetSpartans), new { id = spartans.Id }, ItemToDTO(spartans));
        }

        // DELETE: api/Spartans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpartans(long id)
        {
            var spartans = await _context.Spartans.FindAsync(id);
            if (spartans == null)
            {
                return NotFound();
            }

            _context.Spartans.Remove(spartans);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpartansExists(long id)
        {
            return _context.Spartans.Any(e => e.Id == id);
        }

        private static SpartansDTO ItemToDTO(Spartans spartans) =>
            new SpartansDTO
            {
                Id = spartans.Id,
                Name = spartans.Name,
                Course = spartans.Course
            };
    }
}
