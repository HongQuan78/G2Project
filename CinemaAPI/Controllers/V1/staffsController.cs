using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaObject;

namespace CinemaAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class staffsController : ControllerBase
    {
        private readonly CinemaProject_v4Context _context;

        public staffsController(CinemaProject_v4Context context)
        {
            _context = context;
        }

        // GET: api/staffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<staff>>> Getstaff()
        {
            return await _context.staff.ToListAsync();
        }

        // GET: api/staffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<staff>> Getstaff(int id)
        {
            var staff = await _context.staff.FindAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }

        // PUT: api/staffs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putstaff(int id, staff staff)
        {
            if (id != staff.Id)
            {
                return BadRequest();
            }

            _context.Entry(staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!staffExists(id))
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

        // POST: api/staffs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<staff>> Poststaff(staff staff)
        {
            _context.staff.Add(staff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getstaff", new { id = staff.Id }, staff);
        }

        // DELETE: api/staffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletestaff(int id)
        {
            var staff = await _context.staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.staff.Remove(staff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool staffExists(int id)
        {
            return _context.staff.Any(e => e.Id == id);
        }
    }
}
