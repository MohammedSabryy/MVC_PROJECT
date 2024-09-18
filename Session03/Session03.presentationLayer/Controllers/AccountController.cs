namespace Session03.presentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{
            if (!ModelState.IsValid) return View(model);
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            var result = _userManager.CreateAsync(user, model.Password).Result;
            if(result.Succeeded) 
                return RedirectToAction(nameof(Login));
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null)
            {
                if(_userManager.CheckPasswordAsync(user, model.Password).Result)
                {
                    var result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
                    if (result.Succeeded) return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller",string.Empty));
                }
            }
            ModelState.AddModelError(string.Empty, "InCorrect Email Or Password");
            return View(model);
        }
        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

		public IActionResult ForgetPassword()
		{
            return View();
		}
        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid) return View(model);
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null)
            {
                var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                var url = Url.Action(nameof(ResetPassword),nameof(AccountController).Replace("Controller", string.Empty),
                    new {email = model.Email , Token = token},Request.Scheme);
                var email = new Email
                {
                    Subject = "Reset Password",
                    Body = url!,
                    Recipient = model.Email
                };
                MailSettings.SendEmail(email);
                return RedirectToAction(nameof(CheckYourInbox));
            }
            ModelState.AddModelError(string.Empty, "User Not Found");
            return View(model);
		}
		public IActionResult CheckYourInbox()
		{
			return View();
		}
		public IActionResult ResetPassword()
        {  
            return View(); 
        }
    }
}
