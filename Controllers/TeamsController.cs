using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasySportEvent.Models;

namespace EasySportEvent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ESEContext _context;

        public TeamsController(ESEContext context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            team = GetPoints(team);
            team = SetRating(team);
            _context.Teams.Update(team);
            //_context.Entry(team).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Teams
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            team = GetPoints(team);
            team = SetRating(team);
            _context.Teams.Add(team);

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTeam", new { id = team.Id }, team);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            DeleteEvents(id);
            _context.Teams.Remove(team);
            SetRatingOnDelete(team);
            await _context.SaveChangesAsync();
            return team;
        }

        private void DeleteEvents(int id)
        {
            var eventList = _context.Events.Where(e => e.HomeTeamId == id || e.GuestTeamId == id).ToList();

            foreach(var e in eventList)
            {
                _context.Events.Remove(e);
            }
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }

        private Team GetPoints(Team team)
        {
            int? win = 0;
            int? draw = 0;
            if (team.WinAmount != null) win = team.WinAmount;
            if (team.DrawAmount != null) draw = team.DrawAmount;
            team.PointAmount = win * 3 + draw;
            return team;
        }

        private Team SetRating(Team team)
        {
            var TeamList = _context.Teams.Where(t => t.LeagueId == team.LeagueId && t.Id != team.Id).OrderByDescending(t => t.PointAmount).ToList();

            int k = 1;  
            bool flag = false;
            foreach (var t in TeamList)
            {
                if (team.PointAmount > t.PointAmount && flag == false)
                {
                    team.Rating = k++;
                    t.Rating = k++;
                    flag = true;
                    continue;
                }
                t.Rating = k++;
                _context.Teams.Update(t);
            }

            if (flag == false) team.Rating = k;
            return team;
        }

        private void SetRatingOnDelete(Team team)
        {
            var TeamList = _context.Teams.Where(t => t.LeagueId == team.LeagueId && t.Id != team.Id).OrderByDescending(t => t.PointAmount);

            int k = 1;
            foreach(var t in TeamList)
            {
                t.Rating = k++;
                _context.Teams.Update(t);
            }
        }
    }
}
