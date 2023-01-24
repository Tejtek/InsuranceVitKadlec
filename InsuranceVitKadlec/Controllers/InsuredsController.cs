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
using Microsoft.AspNetCore.Authorization;

namespace InsuranceVitKadlec.Controllers
{
    public class InsuredsController : BaseController
    {
        

        public InsuredsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context) : base(userManager, signInManager, context)
        {
        }



        // GET: Insureds
        [Authorize]
        public async Task<IActionResult> Index() 
        {     
            if (!this.User.IsInRole("Admin"))
            {
                int id = this.GetCurrentInsured().Id;
                return RedirectToAction("Edit", new { id });
            }
            return View(await context.Insured.ToListAsync());
        }

        // GET: Insureds/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Insured == null)
            {
                return NotFound();
            }

            var insured = await context.Insured
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insured == null)
            {
                return NotFound();
            }

            return View(insured);
        }

        // GET: Insureds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insureds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,BirthDate,PhoneNumber,Street,City,PostCode,Smoker,IsMan,Email,PhotoName")] Insured insured)
        {
            if (ModelState.IsValid)
            {
                context.Add(insured);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insured);
        }

        // GET: Insureds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.Insured == null)
            {
                return NotFound();
            }

            var insured = await context.Insured.FindAsync(id);
            if (insured == null)
            {
                return NotFound();
            }
            return View(insured);
        }

        // POST: Insureds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,BirthDate,PhoneNumber,Street,City,PostCode,Smoker,IsMan,Email,PhotoName,LoginId")] Insured insured)
        {
            if (id != insured.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(insured);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuredExists(insured.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Home");
            }
            return View(insured);
        }

        // GET: Insureds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Insured == null)
            {
                return NotFound();
            }

            var insured = await context.Insured
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insured == null)
            {
                return NotFound();
            }

            return View(insured);
        }

        // POST: Insureds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Insured == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Insured'  is null.");
            }
            var insured = await context.Insured.FindAsync(id);
            if (insured != null)
            {
                context.Insured.Remove(insured);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuredExists(int id)
        {
          return context.Insured.Any(e => e.Id == id);
        }
    }
}
