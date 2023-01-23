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
    public class InsuredesInsurencesHistoriesController : BaseController
    {
        public InsuredesInsurencesHistoriesController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context) : base(userManager, signInManager, context)
        {
        }

        // GET: InsuredesInsurencesHistories
        public async Task<IActionResult> Index()

        {
            var items = context.InsuredesInsurencesHistory
                .Include(i => i.Insurence)
                .Include(i => i.Insured)
                .AsQueryable();
            return View(await items.ToListAsync());
        }
                
    }
}
