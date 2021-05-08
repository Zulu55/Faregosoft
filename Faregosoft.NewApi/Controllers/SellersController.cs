using Faregosoft.NewApi.Data;
using Faregosoft.NewApi.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faregosoft.NewApi.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class SellersController : ControllerBase
    {
        private readonly DataContext _context;

        public SellersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Sellers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seller>>> GetSellers()
        {
            return await _context.Sellers.ToListAsync();
        }

        // GET: api/Sellers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSeller(int id)
        {
            Seller seller = await _context.Sellers.FindAsync(id);

            if (seller == null)
            {
                return NotFound();
            }

            return seller;
        }

        // PUT: api/Sellers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeller(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }

            _context.Entry(seller).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellerExists(id))
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

        // POST: api/Sellers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Seller>> PostSeller(Seller seller)
        {
            _context.Sellers.Add(seller);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeller", new { id = seller.Id }, seller);
        }

        // DELETE: api/Sellers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            Seller seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SellerExists(int id)
        {
            return _context.Sellers.Any(e => e.Id == id);
        }
    }
}
