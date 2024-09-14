

namespace Session03.presentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IDepartmentRepository _DepartmentRepository;

        public EmployeesController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _EmployeeRepository = employeeRepository;
            _DepartmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {
            var employees = _EmployeeRepository.GetAllWithDepartments();
            return View(employees);
        }

        public IActionResult Create()
        {
            var Departments = _DepartmentRepository.GetAll();
            SelectList listItems = new SelectList(Departments,"Id","Name");
            ViewBag.Departments = listItems;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {


            // Server Side Validation

            if (!ModelState.IsValid) return View(model: employee);
            _EmployeeRepository.Create(entity: employee);
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
                    _EmployeeRepository.Update(employee);
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

                _EmployeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));


            }
            return View(employee);
        }
        private IActionResult EmployeeControllerHandler(int? id, string viewName)
        {
            if (viewName == nameof(Edit))
            {
                var Departments = _DepartmentRepository.GetAll();
                SelectList listItems = new SelectList(Departments, "Id", "Name");
                ViewBag.Departments = listItems;
            }
            if (!id.HasValue) return BadRequest();
            var employee = _EmployeeRepository.Get(id.Value);
            if (employee is null) return NotFound();
            return View(viewName, employee);
        }

    }
}

