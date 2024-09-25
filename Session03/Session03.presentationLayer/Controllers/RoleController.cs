using Microsoft.AspNetCore.Mvc;

namespace Session03.presentationLayer.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if(!ModelState.IsValid) return View(model);
            var role = new IdentityRole
            {
                Name = model.Name
            };
            var result = await _roleManager.CreateAsync(role);
            if(result.Succeeded)return RedirectToAction(nameof(Index));
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var roles = await _roleManager.Roles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToListAsync();
                return View(roles);
            }
            var Role = await _roleManager.FindByNameAsync(name);
            if (Role == null)
            {
                return View(Enumerable.Empty<RoleViewModel>());
            }
            var model = new RoleViewModel
            {
                Id = Role.Id,
                Name = Role.Name
            };
            return View(model);
        }

        public async Task<IActionResult> Details(string id, string ViewName = nameof(Details))
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null)
            {
                return View(Enumerable.Empty<RoleViewModel>());
            }
            var model = new RoleViewModel
            {
                Id = Role.Id,
                Name = Role.Name
            };
            return View(ViewName, model);

        }

        public async Task<IActionResult> Edit(string id) => await Details(id, nameof(Edit));


        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            try
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role is null) return NotFound();
                role.Name = model.Name;
                await _roleManager.FindByIdAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(string id) => await Details(id, nameof(Delete));

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return NotFound();

                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

    }
}
