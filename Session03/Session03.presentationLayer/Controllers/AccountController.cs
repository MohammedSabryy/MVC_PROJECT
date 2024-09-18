namespace Session03.presentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
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
	}
}
