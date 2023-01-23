using InsuranceVitKadlec.Data;
using InsuranceVitKadlec.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace InsuranceVitKadlec.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly UserManager<IdentityUser> userManager;
        protected readonly SignInManager<IdentityUser> signInManager;
        protected readonly ApplicationDbContext context;

        public BaseController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        protected Insured GetCurrentInsured()
        {
            
            string IdUser = this.userManager.GetUserId(this.User);
            // když nebude přihlášený nikdo
            if (string.IsNullOrEmpty(IdUser)) 
                return null;
            
            return context.Insured
                .AsNoTracking() //nedívej se na změny
                .Where(i => i.LoginId == IdUser)
                .FirstOrDefault();                
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {   
            if (this.User.IsInRole("Admin"))
            {
                ViewBag.FullName =  "Admin";
            }
            else
            {
                // když bude = null -> ? (bez ? nahlásí chybu) = FullName = null )
                ViewBag.FullName = this.GetCurrentInsured()?.FullName??String.Empty;
            }
            base.OnActionExecuting(context);
        }
    }
}
