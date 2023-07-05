using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProtocolsAPI_DanielRamírez.Attributes;
using MyProtocolsAPI_DanielRamírez.Models;

namespace MyProtocolsAPI_DanielRamírez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class ProtocolsController : ControllerBase
    {
        private readonly MyProtocolsDBContext _context;

        public ProtocolsController(MyProtocolsDBContext context)
        {
            _context = context;
        }

        // GET: api/Protocols
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Protocol>>> GetProtocols()
        {
          if (_context.Protocols == null)
          {
              return NotFound();
          }
            return await _context.Protocols.ToListAsync();
        }

        // GET: api/Protocols/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Protocol>> GetProtocol(int id)
        {
          if (_context.Protocols == null)
          {
              return NotFound();
          }
            var protocol = await _context.Protocols.FindAsync(id);

            if (protocol == null)
            {
                return NotFound();
            }

            return protocol;
        }

        // PUT: api/Protocols/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProtocol(int id, Protocol protocol)
        {
            if (id != protocol.ProtocolId)
            {
                return BadRequest();
            }

            _context.Entry(protocol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProtocolExists(id))
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

        // POST: api/Protocols
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Protocol>> PostProtocol(Protocol protocol)
        {
          if (_context.Protocols == null)
          {
              return Problem("Entity set 'MyProtocolsDBContext.Protocols'  is null.");
          }
            _context.Protocols.Add(protocol);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProtocol", new { id = protocol.ProtocolId }, protocol);
        }

        // DELETE: api/Protocols/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProtocol(int id)
        {
            if (_context.Protocols == null)
            {
                return NotFound();
            }
            var protocol = await _context.Protocols.FindAsync(id);
            if (protocol == null)
            {
                return NotFound();
            }

            _context.Protocols.Remove(protocol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProtocolExists(int id)
        {
            return (_context.Protocols?.Any(e => e.ProtocolId == id)).GetValueOrDefault();
        }
    }
}
