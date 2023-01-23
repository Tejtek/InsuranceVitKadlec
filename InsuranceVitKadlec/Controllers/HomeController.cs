using InsuranceVitKadlec.Data;
using InsuranceVitKadlec.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InsuranceVitKadlec.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context) : base(userManager, signInManager, context)
        {
        }

        
        // GET: Articles
        public async Task<IActionResult> Index()
        {
            return View(await context.Article.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}