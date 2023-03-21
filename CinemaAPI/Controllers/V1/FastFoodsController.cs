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
    public class FastFoodsController : ControllerBase
    {
        private readonly CinemaProject_v4Context _context;

        public FastFoodsController(CinemaProject_v4Context context)
        {
            _context = context;
        }

        // GET: api/FastFoods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FastFood>>> GetFastFoods()
        {
            return await _context.FastFoods.ToListAsync();
        }

        // GET: api/FastFoods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FastFood>> GetFastFood(int id)
        {
            var fastFood = await _context.FastFoods.FindAsync(id);

            if (fastFood == null)
            {
                return NotFound();
            }

            return fastFood;
        }

        // PUT: api/FastFoods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFastFood(int id, FastFood fastFood)
        {
            if (id != fastFood.Id)
            {
                return BadRequest();
            }

            _context.Entry(fastFood).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FastFoodExists(id))
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

        // POST: api/FastFoods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FastFood>> PostFastFood(FastFood fastFood)
        {
            _context.FastFoods.Add(fastFood);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFastFood", new { id = fastFood.Id }, fastFood);
        }

        // DELETE: api/FastFoods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFastFood(int id)
        {
            var fastFood = await _context.FastFoods.FindAsync(id);
            if (fastFood == null)
            {
                return NotFound();
            }

            _context.FastFoods.Remove(fastFood);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FastFoodExists(int id)
        {
            return _context.FastFoods.Any(e => e.Id == id);
        }
    }
}
