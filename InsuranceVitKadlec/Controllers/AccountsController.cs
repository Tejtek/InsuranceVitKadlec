using InsuranceVitKadlec.Data;
using InsuranceVitKadlec.Models;
using InsuranceVitKadlec.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceVitKadlec.Controllers
{
    public class AccountsController : BaseController
    {
        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context) : base(userManager, signInManager, context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register model)
        {
          
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    
                    //string userId = this.userManager.GetUserId(this.User);

                    Insured insured = new Insured()
                    {
                        Name = model.Name,
                        Surname= model.Surname,
                        BirthDate= model.BirthDate,
                        PhoneNumber=model.PhoneNumber,
                        Street=model.Street,
                        City= model.City,
                        PostCode=model.PostCode,
                        Smoker=model.Smoker,
                        IsMan= model.IsMan,
                        Email=model.Email,
                        PhotoName=model.PhotoName,
                        LoginId = user.Id
                        
                    };
                    context.Insured.Add(insured);
                    context.SaveChanges();
                    
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError(string.Empty, "Přihlášení proběhlo neúspěšně.");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
