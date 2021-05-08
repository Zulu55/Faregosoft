using Faregosoft.NewApi.Data;
using Faregosoft.NewApi.Data.Entities;
using Faregosoft.NewApi.Helpers;
using Faregosoft.NewApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Faregosoft.NewApi.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProductImagesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;

        public ProductImagesController(DataContext context, IBlobHelper blobHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
        }

        [HttpPost]
        public async Task<ActionResult<AddProductImageRequest>> PostProductImage(AddProductImageRequest model)
        {
            Product product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == model.ProductId);
            if (product == null)
            {
                return BadRequest("Producto no existe.");
            }

            Guid image = await _blobHelper.UploadBlobAsync(model.Image, "products");
            if (product.ProductImages == null)
            {
                product.ProductImages = new List<ProductImage>();
            }

            product.ProductImages.Add(new ProductImage
            {
                Image = image
            });

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            model.Guid = image;
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            ProductImage productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }

            await _blobHelper.DeleteBlobAsync(productImage.Image, "products");
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
