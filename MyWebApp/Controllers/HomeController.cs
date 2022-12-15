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
    
    public class HomeController : ControllerBase
    {
        private MyWebApp_JWTContext db { get; }
        public HomeController(MyWebApp_JWTContext db)
        {
            this.db = db;
        }
        
        // GET: api/<Home>
        [HttpGet]
        public IEnumerable<GetDataView> GetAllProducts()
        {
            var dataList = (from m in db.ProductImage
                            join p in db.Products on m.ProductId equals p.Id
                            join c in db.Category on p.CategoryId equals c.Id
                            select new GetDataView() {
                            Id=m.Id,
                            ImagePath=m.ImagePath,
                            ProductName=p.ProductName,
                            CategoryName=c.CategoryName,
                            Quantity=p.Quantity,
                            UnitPrice=p.UnitPrice,
                            StoreDate=p.StoreDate,
                            IsAvailable=(bool)p.IsAvailable
                            }).ToList();
            return dataList;
        }
        

    }
}
