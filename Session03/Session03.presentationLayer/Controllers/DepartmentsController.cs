using Microsoft.AspNetCore.Mvc;
using Session03.BusinessLogicLayer.Interfaces;
using Session03.DataAccessLayer.Models;

namespace Session03.presentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _repository = departmentRepository;
        }

        public IActionResult Index()
        {
            var deprtments = _repository.GetAll();
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
            _repository.Create(entity: department);
            return RedirectToAction(actionName: nameof(Index));
        }
        public IActionResult Details(int? id) => DepartmentControllerHandler(id,nameof(Details));
        
        public IActionResult Edit(int? id) => DepartmentControllerHandler(id,nameof(Edit));


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
        public IActionResult Delete(int? id) => DepartmentControllerHandler(id,nameof(Delete));

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
        private IActionResult DepartmentControllerHandler(int? id,string viewName)
        {

            if (!id.HasValue) return BadRequest();
            var department = _repository.Get(id.Value);
            if (department is null) return NotFound();
            return View(viewName,department);
        }

    }
}
