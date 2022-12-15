using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private MyWebApp_JWTContext db { get; }
        private readonly IWebHostEnvironment env;
        public ProductImageController(MyWebApp_JWTContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        // GET: api/<ProductImage>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImage>>> GetProductImages()
        {

            return await db.ProductImage.ToListAsync();
        }

        // GET api/<ProductImage>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImage>> GetImageDataById(int id)
        {
            var img = await db.ProductImage.FindAsync(id);

            if (img == null)
            {
                return NotFound();
            }

            return img;
        }

        // POST api/<ProductImage>
        [HttpPost]
        public async Task<ActionResult<ProductImage>> PostProductImage(ProductImage image)
        {
            db.ProductImage.Add(image);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetImageDataById", new { id = image.Id }, image);
        }


        // PUT api/<ProductImage>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(int id, ProductImage image)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }

            db.Entry(image).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
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

        // DELETE api/<ProductImage>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductImage>> DeleteProductImage(int id)
        {
            var productImage = await db.ProductImage.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }

            db.ProductImage.Remove(productImage);
            await db.SaveChangesAsync();

            return productImage;
        }
        private bool ImageExists(int id)
        {
            return db.ProductImage.Any(e => e.Id == id);
        }

        [HttpPost("Uploads/{id}")]
        public async Task<ActionResult<ImagePathResponse>> PostImage(int id, IFormFile file)
        {
            var proImage = await db.ProductImage.FindAsync(id);
            if (proImage == null)
            {
                return NotFound();
            }
            try
            {
                string ext = Path.GetExtension(file.FileName);
                string f = Guid.NewGuid() + ext;
                if (!Directory.Exists(env.WebRootPath + "\\Images\\"))
                {
                    Directory.CreateDirectory(env.WebRootPath + "\\Images\\");
                }
                using FileStream fileStream = System.IO.File.Create(env.WebRootPath + "\\Images\\" + f);
                file.CopyTo(fileStream);
                fileStream.Flush();
                proImage.ImagePath = f;
                fileStream.Close();
                await db.SaveChangesAsync();
                return new ImagePathResponse { PicturePath = f };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
