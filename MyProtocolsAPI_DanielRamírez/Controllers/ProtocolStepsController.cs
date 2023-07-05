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
    public class ProtocolStepsController : ControllerBase
    {
        private readonly MyProtocolsDBContext _context;

        public ProtocolStepsController(MyProtocolsDBContext context)
        {
            _context = context;
        }

        // GET: api/ProtocolSteps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProtocolStep>>> GetProtocolSteps()
        {
          if (_context.ProtocolSteps == null)
          {
              return NotFound();
          }
            return await _context.ProtocolSteps.ToListAsync();
        }

        // GET: api/ProtocolSteps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProtocolStep>> GetProtocolStep(int id)
        {
          if (_context.ProtocolSteps == null)
          {
              return NotFound();
          }
            var protocolStep = await _context.ProtocolSteps.FindAsync(id);

            if (protocolStep == null)
            {
                return NotFound();
            }

            return protocolStep;
        }

        // PUT: api/ProtocolSteps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProtocolStep(int id, ProtocolStep protocolStep)
        {
            if (id != protocolStep.ProtocolStepsId)
            {
                return BadRequest();
            }

            _context.Entry(protocolStep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProtocolStepExists(id))
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

        // POST: api/ProtocolSteps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProtocolStep>> PostProtocolStep(ProtocolStep protocolStep)
        {
          if (_context.ProtocolSteps == null)
          {
              return Problem("Entity set 'MyProtocolsDBContext.ProtocolSteps'  is null.");
          }
            _context.ProtocolSteps.Add(protocolStep);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProtocolStep", new { id = protocolStep.ProtocolStepsId }, protocolStep);
        }

        // DELETE: api/ProtocolSteps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProtocolStep(int id)
        {
            if (_context.ProtocolSteps == null)
            {
                return NotFound();
            }
            var protocolStep = await _context.ProtocolSteps.FindAsync(id);
            if (protocolStep == null)
            {
                return NotFound();
            }

            _context.ProtocolSteps.Remove(protocolStep);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProtocolStepExists(int id)
        {
            return (_context.ProtocolSteps?.Any(e => e.ProtocolStepsId == id)).GetValueOrDefault();
        }
    }
}
