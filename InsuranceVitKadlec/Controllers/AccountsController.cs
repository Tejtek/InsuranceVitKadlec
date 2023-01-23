using InsuranceVitKadlec.Data;
using InsuranceVitKadlec.Models;
using InsuranceVitKadlec.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceVitKadlec.Controllers
{
    public class AccountsController : BaseController
    {
        private IWebHostEnvironment webHostEnvironment;

        public AccountsController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context,
            // zajištění cesty k wwwroot
            IWebHostEnvironment webHostEnvironment) : base(userManager, signInManager, context)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public string SaveFile(IFormFile formFile)
        {
            if (formFile == null)
                return null;
            string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(formFile.FileName);
            string imagesDirectory = Path.Combine(
                this.webHostEnvironment.WebRootPath,
                "imagesClients");

            string fullFileName = Path.Combine(imagesDirectory, fileName);

            using (var stream = new FileStream(fullFileName, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            return fileName;
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
                if(model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Heslo není shodné s potvrzením.");
                    return View(model);
                }
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
                        PhotoName=SaveFile(model.Picture), 
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
