


using Session03.DataAccessLayer.Models;

namespace Session03.presentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IDepartmentRepository _DepartmentRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _EmployeeRepository = employeeRepository;
            _DepartmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var employees = _EmployeeRepository.GetAllWithDepartments();
            var employeeVM = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            return View(employeeVM);
        }

        public IActionResult Create()
        {
            var Departments = _DepartmentRepository.GetAll();
            SelectList listItems = new SelectList(Departments,"Id","Name");
            ViewBag.Departments = listItems;
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {


            // Server Side Validation
            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            if (!ModelState.IsValid) return View(model: employeeVM);
            _EmployeeRepository.Create(entity: employee);
            return RedirectToAction(actionName: nameof(Index));
        }
        public IActionResult Details(int? id) => EmployeeControllerHandler(id, nameof(Details));

        public IActionResult Edit(int? id) => EmployeeControllerHandler(id, nameof(Edit));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                    _EmployeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employeeVM);
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
            //var EmployeeVM = new EmployeeViewModel
            //{
            //    Address = employee.Address,
            //    Department = employee.Department,
            //    Age = employee.Age,
            //    DepartmentId = employee.DepartmentId,
            //    Name = employee.Name,
            //    Email = employee.Email,
            //    Id = employee.Id,
            //    IsActive = employee.IsActive,
            //    Phone = employee.Phone,
            //    Salary = employee.Salary,
            //};
            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);
            return View(viewName, employeeVM);
        }

    }
}

