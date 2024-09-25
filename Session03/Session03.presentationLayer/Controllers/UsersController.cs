using Microsoft.AspNetCore.Mvc;

namespace Session03.presentationLayer.Controllers
{
	public class UsersController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UsersController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task <IActionResult> Index(string email)
		{
			if (string.IsNullOrWhiteSpace(email)) 
			{
				var users = await _userManager.Users.Select(u=> new UserViewModel 
				{
					Email = u.Email,
					FirstName = u.FirstName,
					LastName = u.LastName,
					Id = u.Id,
					UserName = u.UserName,
					Roles = _userManager.GetRolesAsync(u).GetAwaiter().GetResult()
				}).ToListAsync();
				return View(users);
			}
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null) 
			{
				return View(Enumerable.Empty<UserViewModel>());
			}
			var model = new UserViewModel
			{
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Id = user.Id,
				UserName = user.UserName,
				Roles = await _userManager.GetRolesAsync(user)
			};
			return View(model);
		}
	}
}
