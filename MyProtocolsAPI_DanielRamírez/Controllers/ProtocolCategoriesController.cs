using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProtocolsAPI_DanielRamírez.Models;
using MyProtocolsAPI_DanielRamírez.Attributes;

namespace MyProtocolsAPI_DanielRamírez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class ProtocolCategoriesController : ControllerBase
    {
        private readonly MyProtocolsDBContext _context;

        public ProtocolCategoriesController(MyProtocolsDBContext context)
        {
            _context = context;
        }

        // GET: api/ProtocolCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProtocolCategory>>> GetProtocolCategories()
        {
          if (_context.ProtocolCategories == null)
          {
              return NotFound();
          }
            return await _context.ProtocolCategories.ToListAsync();
        }

        // GET: api/ProtocolCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProtocolCategory>> GetProtocolCategory(int id)
        {
          if (_context.ProtocolCategories == null)
          {
              return NotFound();
          }
            var protocolCategory = await _context.ProtocolCategories.FindAsync(id);

            if (protocolCategory == null)
            {
                return NotFound();
            }

            return protocolCategory;
        }

        // PUT: api/ProtocolCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProtocolCategory(int id, ProtocolCategory protocolCategory)
        {
            if (id != protocolCategory.ProtocolCategory1)
            {
                return BadRequest();
            }

            _context.Entry(protocolCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProtocolCategoryExists(id))
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

        // POST: api/ProtocolCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProtocolCategory>> PostProtocolCategory(ProtocolCategory protocolCategory)
        {
          if (_context.ProtocolCategories == null)
          {
              return Problem("Entity set 'MyProtocolsDBContext.ProtocolCategories'  is null.");
          }
            _context.ProtocolCategories.Add(protocolCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProtocolCategory", new { id = protocolCategory.ProtocolCategory1 }, protocolCategory);
        }

        // DELETE: api/ProtocolCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProtocolCategory(int id)
        {
            if (_context.ProtocolCategories == null)
            {
                return NotFound();
            }
            var protocolCategory = await _context.ProtocolCategories.FindAsync(id);
            if (protocolCategory == null)
            {
                return NotFound();
            }

            _context.ProtocolCategories.Remove(protocolCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProtocolCategoryExists(int id)
        {
            return (_context.ProtocolCategories?.Any(e => e.ProtocolCategory1 == id)).GetValueOrDefault();
        }
    }
}
