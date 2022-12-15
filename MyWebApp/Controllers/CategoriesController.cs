using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyWebApp_JWTContext db;
        public CategoriesController(MyWebApp_JWTContext db)
        {
            this.db = db;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await db.Category.ToListAsync();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryId(int id)
        {
            var category = await db.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }
        // PUT api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
        
        // POST api/Categories
        
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            db.Category.Add(category);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }



        // DELETE api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Category.Remove(category);
            await db.SaveChangesAsync();

            return category;
        }
        private bool CategoryExists(int id)
        {
            return db.Category.Any(e => e.Id == id);
        }
    }
}
