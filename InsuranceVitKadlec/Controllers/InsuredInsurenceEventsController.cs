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
    public class InsuredInsurenceEventsController : BaseController
    {
       
        public InsuredInsurenceEventsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context) : base(userManager, signInManager, context)
        {
        }


        // GET: InsuredInsurenceEvents
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var items = context.InsuredInsurenceEvent
                .Include(i => i.InsuredesInsurences)
                .Include(i => i.InsuredesInsurences.Insurence)
                .Include(i => i.InsuredesInsurences.Insured)
                .AsQueryable();
            if (!this.User.IsInRole("Admin"))
            {
                Insured insured = this.GetCurrentInsured();

                items = items
                    .Where(i => i.InsuredesInsurences.InsuredId == insured.Id).AsQueryable();
            }

            return View(await items.ToListAsync());
        }

        // GET: InsuredInsurenceEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.InsuredInsurenceEvent == null)
            {
                return NotFound();
            }

            var insuredInsurenceEvent = await context.InsuredInsurenceEvent
                .Include(i => i.InsuredesInsurences)
                .Include(i => i.InsuredesInsurences.Insurence)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuredInsurenceEvent == null)
            {
                return NotFound();
            }

            return View(insuredInsurenceEvent);
        }

        // GET: InsuredInsurenceEvents/Create
        public IActionResult Create()
        {
            var insuredInsurences = context.InsuredesInsurences
                .Include(i=> i.Insurence)
                .AsQueryable();

            if (!this.User.IsInRole("Admin"))
            {
                Insured insured = this.GetCurrentInsured();

                insuredInsurences = insuredInsurences.
                    Where (i => i.InsuredId == insured.Id).AsQueryable();
            }

            ViewData["InsuredesInsurencesId"] = new SelectList(insuredInsurences, "Id", "Insurence.Name");
            return View();

        }

        // POST: InsuredInsurenceEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InsuredesInsurencesId,Name,Description,InsurenceDamage,DamageDate")] InsuredInsurenceEvent insuredInsurenceEvent)
        {
            if (ModelState.IsValid)
            {
                context.Add(insuredInsurenceEvent);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuredInsurenceEvent);
        }

        // GET: InsuredInsurenceEvents/Edit/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.InsuredInsurenceEvent == null)
            {
                return NotFound();
            }

            var insuredInsurenceEvent = await context.InsuredInsurenceEvent
                .Include(i=> i.InsuredesInsurences.Insured)
                .FirstOrDefaultAsync(i=> i.Id == id);
            if (insuredInsurenceEvent == null)
            {
                return NotFound();
            }
            var insuredInsurences = context.InsuredesInsurences.AsQueryable();
                        
            ViewData["InsuredesInsurencesId"] = new SelectList(insuredInsurences, "Id", "InsurenceSubject");
         

            return View(insuredInsurenceEvent);
        }

        // POST: InsuredInsurenceEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InsuredesInsurencesId,Name,Description,InsurenceDamage,DamageDate")] InsuredInsurenceEvent insuredInsurenceEvent)
        {
            if (id != insuredInsurenceEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(insuredInsurenceEvent);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuredInsurenceEventExists(insuredInsurenceEvent.Id))
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
            return View(insuredInsurenceEvent);
        }

        // GET: InsuredInsurenceEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.InsuredInsurenceEvent == null)
            {
                return NotFound();
            }

            var insuredInsurenceEvent = await context.InsuredInsurenceEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuredInsurenceEvent == null)
            {
                return NotFound();
            }

            return View(insuredInsurenceEvent);
        }

        // POST: InsuredInsurenceEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.InsuredInsurenceEvent == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InsuredInsurenceEvent'  is null.");
            }
            var insuredInsurenceEvent = await context.InsuredInsurenceEvent.FindAsync(id);
            if (insuredInsurenceEvent != null)
            {
                context.InsuredInsurenceEvent.Remove(insuredInsurenceEvent);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuredInsurenceEventExists(int id)
        {
          return context.InsuredInsurenceEvent.Any(e => e.Id == id);
        }
    }
}
