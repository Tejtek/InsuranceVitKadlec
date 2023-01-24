using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsuranceVitKadlec.Data;
using InsuranceVitKadlec.Models;
using Microsoft.AspNetCore.Identity;

namespace InsuranceVitKadlec.Controllers
{
    public class InsuredHistoriesController : BaseController
    {
      

        public InsuredHistoriesController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context) : base(userManager, signInManager, context)
        {
        }

        // GET: InsuredHistories
        public async Task<IActionResult> Index()
        {
            return View(await context.InsuredHistory.ToListAsync());
        }


    }  
       
}
