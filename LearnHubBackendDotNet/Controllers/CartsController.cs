using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnHubBackendDotNet.Data;
using LearnHubBackendDotNet.Models;
using LearnHubBackendDotNet.DTO;
using LearnHubBackendDotNet.Exceptions;

namespace LearnHubBackendDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartsController(AppDbContext context)
        {
            _context = context;
        }

        //Get cart items data using UserId  GET: api/Carts/5/added
        [HttpGet("{userId}/{status}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartItems(int userId,string status)
        {
          if (_context.CartItems == null)
          {
              return NotFound();
          }
                return await _context.CartItems
                .Where(c => c.userId == userId && c.status==status )
                .ToListAsync();                    
        }


        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateCartStatus(int id,[FromBody] CartStatusUpdateRequestDto dto)
        {            
            try
            {
                var cartItem = await _context.CartItems.FindAsync(id);

                if (cartItem == null)
                {
                    throw new NotFoundException("Cart item not found");
                }
                else
                {
                    cartItem.status=dto.status;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Update success");
        }

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
          if (_context.CartItems == null)
          {
              return Problem("Entity set 'AppDbContext.CartItems'  is null.");
          }
            var existingCart = await _context.CartItems
                .Where(c => c.userId == cart.userId && c.courseId == cart.courseId)
                .FirstOrDefaultAsync();
            if (existingCart == null)
            {
                _context.CartItems.Add(cart);
                await _context.SaveChangesAsync();

                return Ok("Item Added");
            }
            else
            {
                return BadRequest("Item already exists in the cart");
            }
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (_context.CartItems == null)
            {
                return NotFound();
            }
            var cart = await _context.CartItems.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cart);
            await _context.SaveChangesAsync();

            return Ok("Delete success");
        }

        private bool CartExists(int id)
        {
            return (_context.CartItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
