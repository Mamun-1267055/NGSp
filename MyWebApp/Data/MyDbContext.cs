using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Data
{
    public class MyDbContext:IdentityDbContext<AppUser, IdentityRole, string>
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
