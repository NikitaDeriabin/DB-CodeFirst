using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasySportEvent.Models;
using System.Text.RegularExpressions;

namespace EasySportEvent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        private readonly ESEContext _context;

        public SportsController(ESEContext context)
        {
            _context = context;
        }

        // GET: api/Sports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sport>>> GetSports()
        {
            return await _context.Sports.ToListAsync();
        }

        // GET: api/Sports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sport>> GetSport(int id)
        {
            var sport = await _context.Sports.FindAsync(id);

            if (sport == null)
            {
                return NotFound();
            }

            return sport;
        }

        // PUT: api/Sports/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSport(int id, Sport sport)
        {
            if (id != sport.Id)
            {
                return BadRequest();
            }

            string pattern = @"^[A-Z]+[a-zA-Z""'\s-]*$";
            if (sport.Name.Length < 3 || sport.Name.Length > 21)
            {
                ModelState.AddModelError("Name", "Довжина має бути від 3 до 20 символів");
            }
            if (!Regex.IsMatch(sport.Name, pattern))
            {
                ModelState.AddModelError("Name", "Ви можете ввести тільки літери латиниці та пробіл. Перша буква повинна бути прописною");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(sport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportExists(id))
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

        // POST: api/Sports
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sport>> PostSport(Sport sport)
        {
            string pattern = @"^[A-Z]+[a-zA-Z""'\s-]*$";
            if (sport.Name.Length < 3 || sport.Name.Length > 21)
            {
                ModelState.AddModelError("Name", "Довжина має бути від 3 до 20 символів");
            }
            if(!Regex.IsMatch(sport.Name, pattern))
            {
                ModelState.AddModelError("Name", "Ви можете ввести тільки літери латиниці та пробіл. Перша буква повинна бути прописною");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Sports.Add(sport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSport", new { id = sport.Id }, sport);
        }

        // DELETE: api/Sports/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sport>> DeleteSport(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            DeleteRegions(id);

            _context.Sports.Remove(sport);
            await _context.SaveChangesAsync();

            return sport;
        }

        private void DeleteRegions(int id)
        {
            var regionsList = _context.Regions.Where(r => r.SportId == id).ToList();

            foreach(var r in regionsList)
            {
                r.SportId = null;
            }
        }
        private bool SportExists(int id)
        {
            return _context.Sports.Any(e => e.Id == id);
        }
    }
}
