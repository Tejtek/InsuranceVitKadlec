using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsuranceVitKadlec.Data;
using InsuranceVitKadlec.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace InsuranceVitKadlec.Controllers
{
    public class InsuredesInsurencesController : BaseController

    {
        
        public InsuredesInsurencesController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context) : base(userManager, signInManager, context)
        {
        }



        // GET: InsuredesInsurences
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var items = context.InsuredesInsurences
                .Include(i => i.Insured)
                .Include(i => i.Insurence).AsQueryable(); // převede na iQueryable

            if (!this.User.IsInRole("Admin"))
            {
                Insured insured = this.GetCurrentInsured();

                items = items.Where(i => i.InsuredId == insured.Id).AsQueryable();
            }
            
            return View(await items.ToListAsync());
        }

        // GET: InsuredesInsurences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.InsuredesInsurences == null)
            {
                return NotFound();
            }

            var insuredesInsurences = await context.InsuredesInsurences
                .Include(i => i.Insured)
                .Include(i => i.Insurence)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuredesInsurences == null)
            {
                return NotFound();
            }

            return View(insuredesInsurences);
        }

        // GET: InsuredesInsurences/Create
        public IActionResult Create()
        {
            var insureds = context.Insured.AsQueryable();

            if (!this.User.IsInRole("Admin"))
            {
                Insured insured = this.GetCurrentInsured();

                insureds = insureds.Where(i => i.Id == insured.Id).AsQueryable();
            }

            ViewData["InsuredId"] = new SelectList(insureds, "Id", "Name");
            ViewData["InsurenceId"] = new SelectList(context.Insurence, "Id", "Name");
            return View();
        }

        // POST: InsuredesInsurences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InsuredId,InsurenceId,InsurenceValue,InsurenceSubject,ValidFrom,ValidTo")] InsuredesInsurences insuredesInsurences)
        {
            if (ModelState.IsValid)
            {
                context.Add(insuredesInsurences);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuredId"] = new SelectList(context.Insured, "Id", "Id", insuredesInsurences.InsuredId);
            ViewData["InsurenceId"] = new SelectList(context.Insurence, "Id", "Id", insuredesInsurences.InsurenceId);
            return View(insuredesInsurences);
        }

        // GET: InsuredesInsurences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.InsuredesInsurences == null)
            {
                return NotFound();
            }

            var insuredesInsurences = await context.InsuredesInsurences.FindAsync(id);
            if (insuredesInsurences == null)
            {
                return NotFound();
            }
            ViewData["InsuredId"] = new SelectList(context.Insured, "Id", "FullName", insuredesInsurences.InsuredId);
            ViewData["InsurenceId"] = new SelectList(context.Insurence, "Id", "Name", insuredesInsurences.InsurenceId);
            return View(insuredesInsurences);
        }

        // POST: InsuredesInsurences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InsuredId,InsurenceId,InsurenceValue,InsurenceSubject,ValidFrom,ValidTo")] InsuredesInsurences insuredesInsurences)
        {
            if (id != insuredesInsurences.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(insuredesInsurences);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuredesInsurencesExists(insuredesInsurences.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuredId"] = new SelectList(context.Insured, "Id", "Id", insuredesInsurences.InsuredId);
            ViewData["InsurenceId"] = new SelectList(context.Insurence, "Id", "Id", insuredesInsurences.InsurenceId);
            return View(insuredesInsurences);
        }

        // GET: InsuredesInsurences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.InsuredesInsurences == null)
            {
                return NotFound();
            }

            var insuredesInsurences = await context.InsuredesInsurences
                .Include(i => i.Insured)
                .Include(i => i.Insurence)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuredesInsurences == null)
            {
                return NotFound();
            }

            return View(insuredesInsurences);
        }

        // POST: InsuredesInsurences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.InsuredesInsurences == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InsuredesInsurences'  is null.");
            }
            var insuredesInsurences = await context.InsuredesInsurences.FindAsync(id);
            if (insuredesInsurences != null)
            {
                context.InsuredesInsurences.Remove(insuredesInsurences);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuredesInsurencesExists(int id)
        {
          return context.InsuredesInsurences.Any(e => e.Id == id);
        }
    }
}
