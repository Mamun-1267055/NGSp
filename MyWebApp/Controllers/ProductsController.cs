using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Enums;
using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private MyWebApp_JWTContext db { get; }
        public ProductsController(MyWebApp_JWTContext db)
        {
            this.db = db;
        }
        // GET: api/<Products>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetAllProducts()
        {
            return await db.ShowProducts().ToListAsync();
        }

        // GET api/<Products>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProductById(int id)
        {
            var products = await db.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // POST api/<Products>
        //[Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<object> PostProduct([FromBody] Products products)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.InsertProduct(products);
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Data Insert Successfully", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "invalid model", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }


        }
        
        // PUT api/<Products>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Products products)
        {
            db.UpdateProduct(products);
        }
        
        // DELETE api/<Products>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            db.DeleteProduct(id);
        }
    }
}
