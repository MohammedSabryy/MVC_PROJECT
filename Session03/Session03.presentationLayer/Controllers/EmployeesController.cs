using Microsoft.AspNetCore.Mvc;
using Session03.DataAccessLayer.Models;

namespace Session03.presentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _repository = employeeRepository;
        }

        public IActionResult Index()
        {
            var employees = _repository.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {


            // Server Side Validation

            if (!ModelState.IsValid) return View(model: employee);
            _repository.Create(entity: employee);
            return RedirectToAction(actionName: nameof(Index));
        }
        public IActionResult Details(int? id) => EmployeeControllerHandler(id, nameof(Details));

        public IActionResult Edit(int? id) => EmployeeControllerHandler(id, nameof(Edit));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employee);
        }
        public IActionResult Delete(int? id) => EmployeeControllerHandler(id, nameof(Delete));

        [HttpPost]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                _repository.Delete(employee);
                return RedirectToAction(nameof(Index));


            }
            return View(employee);
        }
        private IActionResult EmployeeControllerHandler(int? id, string viewName)
        {

            if (!id.HasValue) return BadRequest();
            var employee = _repository.Get(id.Value);
            if (employee is null) return NotFound();
            return View(viewName, employee);
        }

    }
}

