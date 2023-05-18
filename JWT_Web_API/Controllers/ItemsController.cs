using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JWT_Web_API.DBContextFile;
using JWT_Web_API.Models;
using Microsoft.AspNetCore.Authorization;
using JWT_Web_API.Models.ViewModel;

namespace JWT_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DBContextConn _context;

        public ItemsController(DBContextConn context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            return await _context.Items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Item>> PostItem(ViewItem item)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'DBContextConn.Items'  is null.");
            }
            var additem = new Item()
            {
                Name= item.Name,
                Price= item.Price,
                userId=item.userId
            };
            _context.Items.Add(additem);
            await _context.SaveChangesAsync();

            return Ok("Create");
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (_context.Items == null)
            {
                return NotFound();
            }
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
