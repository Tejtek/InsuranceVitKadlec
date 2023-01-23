using InsuranceVitKadlec.Data;
using InsuranceVitKadlec.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            return context.Insured
                .Where(i => i.LoginId == IdUser)
                .First();            
        }

    }
}
