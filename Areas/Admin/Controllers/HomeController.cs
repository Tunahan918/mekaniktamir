using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using bakimonarim.Data;

namespace bakimonarim.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class HomeController : Controller
    {
        readonly AppDbContext _dbContext;
        readonly IConfiguration _configuration;
        public HomeController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/admin/login");
        }
    }
}