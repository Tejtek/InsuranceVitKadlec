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
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace InsuranceVitKadlec.Controllers
{
    public class InsurencesController : BaseController
    {
        

        public InsurencesController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context) : base(userManager, signInManager, context)
        {
        }


        // GET: Insurences
        public async Task<IActionResult> Index()
        {
              return View(await context.Insurence.ToListAsync());
        }

        // GET: Insurences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Insurence == null)
            {
                return NotFound();
            }

            var insurence = await context.Insurence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurence == null)
            {
                return NotFound();
            }

            return View(insurence);
        }

        // GET: Insurences/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insurences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Insurence insurence)
        {
            if (ModelState.IsValid)
            {
                context.Add(insurence);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insurence);
        }

        // GET: Insurences/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.Insurence == null)
            {
                return NotFound();
            }

            var insurence = await context.Insurence.FindAsync(id);
            if (insurence == null)
            {
                return NotFound();
            }
            return View(insurence);
        }

        // POST: Insurences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Insurence insurence)
        {
            if (id != insurence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(insurence);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsurenceExists(insurence.Id))
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
            return View(insurence);
        }

        // GET: Insurences/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Insurence == null)
            {
                return NotFound();
            }

            var insurence = await context.Insurence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurence == null)
            {
                return NotFound();
            }

            return View(insurence);
        }

        // POST: Insurences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Insurence == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Insurence'  is null.");
            }
            var insurence = await context.Insurence.FindAsync(id);
            if (insurence != null)
            {
                context.Insurence.Remove(insurence);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsurenceExists(int id)
        {
          return context.Insurence.Any(e => e.Id == id);
        }
    }
}
