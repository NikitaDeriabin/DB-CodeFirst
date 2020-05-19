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
    public class EventsController : ControllerBase
    {
        private readonly ESEContext _context;

        public EventsController(ESEContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (@event.Date.AddHours(3) < DateTime.Now) @event.EndedEvent = true;
            else @event.EndedEvent = false;

            if (id != @event.Id)
            {
                return BadRequest();
            }

            //try
            //{
            //    @event = CheckResult(@event);

            //    CheckVersus(@event);
            //}
            //catch(Exception ex)
            //{
            //    ModelState.AddModelError(string.Empty, ex.Message);
            //    throw;
            //}

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            if (@event.Date.AddHours(3) < DateTime.Now) @event.EndedEvent = true;
            else @event.EndedEvent = false;

            try
            {
                @event = CheckResult(@event);

                CheckVersus(@event);

                _context.Events.Add(@event);

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                throw;
            }           
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        private Event CheckResult(Event e)
        {
            if(e.EndedEvent == false && !String.IsNullOrEmpty(e.Result))
            {
                throw new Exception("Подiя не завершена");
            }
            return e;
        }

        private void CheckVersus(Event @event)
        {
            if(@event.HomeTeamId == @event.GuestTeamId && @event.HomeTeamId != null)
            {
                throw new Exception("У команд однаковi id");
            }
            if(@event.HomePersonId == @event.GuestPersonId && @event.HomePersonId != null)
            {
                throw new Exception("У гравцiв однаковi id");
            }
            if((@event.HomeTeamId != null || @event.GuestTeamId != null) && (@event.HomePersonId != null || @event.GuestPersonId != null))
            {
                throw new Exception("У подiї не можуть одночасно брати учать гравцi та команди");
            }

            CheckTimeEvent(@event);
        }

        private void CheckTimeEvent(Event @event)
        {
            foreach(var e in _context.Events)
            {
                if (@event.Id == e.Id) continue;
                if((@event.HomeTeamId == e.HomeTeamId || @event.HomeTeamId == e.GuestTeamId) && @event.HomeTeamId != null)
                {
                    if (e.Date.Day == @event.Date.Day) throw new Exception("У гравця/команди вже є гра в цей день");
                }
                if ((@event.GuestTeamId == e.HomeTeamId || @event.GuestTeamId == e.GuestTeamId) && @event.GuestTeamId != null)
                {
                    if (e.Date.Day == @event.Date.Day) throw new Exception("У гравця/команди вже є гра в цей день");
                }
                if ((@event.HomePersonId == e.HomePersonId || @event.HomePersonId == e.GuestPersonId) && @event.HomePersonId != null)
                {
                    if (e.Date.Day == @event.Date.Day) throw new Exception("У гравця/команди вже є гра в цей день");
                }
                if ((@event.GuestPersonId == e.HomePersonId || @event.GuestPersonId == e.GuestPersonId) && @event.GuestPersonId != null)
                {
                    if (e.Date.Day == @event.Date.Day) throw new Exception("У гравця/команди вже є гра в цей день");
                }
            }
        }
    }
}
