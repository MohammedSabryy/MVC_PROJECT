using Microsoft.AspNetCore.Mvc;
using Session03.BusinessLogicLayer.Interfaces;
using Session03.BusinessLogicLayer.repositories;
using Session03.DataAccessLayer.Models;

namespace Session03.presentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        //private readonly IGenericRepository<Department> _repository;
        private IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _repository = departmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var deprtments =await _repository.GetAllAsync();
            return View(deprtments);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {


            // Server Side Validation

            if (!ModelState.IsValid) return View(model: department);
            _repository.AddAsync(entity: department);
            return RedirectToAction(actionName: nameof(DepartmentsController.Index));
        }
        public async Task<IActionResult> Details(int? id) => await DepartmentControllerHandler(id, nameof(Details));
        
        public async Task<IActionResult> Edit(int? id) => await DepartmentControllerHandler(id, nameof(Edit));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(department);
        }
        public async Task<IActionResult> Delete(int? id) =>await DepartmentControllerHandler(id,nameof(Delete));

        [HttpPost]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                _repository.Delete(department);
                return RedirectToAction(nameof(Index));


            }
            return View(department);
        }
        private async Task<IActionResult> DepartmentControllerHandler(int? id,string viewName)
        {

            if (!id.HasValue) return BadRequest();
            var department =await _repository.GetAsync(id.Value);
            if (department is null) return NotFound();
            return View(viewName,department);
        }

    }
}
