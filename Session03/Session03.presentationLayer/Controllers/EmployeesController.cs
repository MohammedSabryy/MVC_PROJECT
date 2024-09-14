namespace Session03.presentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index(string? SearchValue)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrWhiteSpace(SearchValue))
                employees = _unitOfWork.Employees.GetAllWithDepartments();
            else employees = _unitOfWork.Employees.GetAll(SearchValue);
            var employeeVM = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(employeeVM); 
            
        }

        public IActionResult Create()
        {
            var Departments = _unitOfWork.Departments.GetAll();
            SelectList listItems = new SelectList(Departments,"Id","Name");
            ViewBag.Departments = listItems;
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {


            // Server Side Validation
            if (!ModelState.IsValid) return View(model: employeeVM);
            if(employeeVM.Image is not null)
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            _unitOfWork.Employees.Create(entity: employee);
            _unitOfWork.saveChanges();
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
                    if (employeeVM.Image is not null)
                        employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                    var employee = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                    _unitOfWork.Employees.Update(employee);
                    if (_unitOfWork.saveChanges() > 0)
                    {
                        TempData["Message"] = $"Employee {employeeVM.Name} Updated Successfully";
                    }
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

                _unitOfWork.Employees.Delete(employee);
                if(_unitOfWork.saveChanges()>0 && employee.ImageName is not null)
                {
                    DocumentSettings.DeleteFile("Images", employee.ImageName);
                }
                return RedirectToAction(nameof(Index));


            }
            return View(employee);
        }
        private IActionResult EmployeeControllerHandler(int? id, string viewName)
        {
            if (viewName == nameof(Edit))
            {
                var Departments = _unitOfWork.Departments.GetAll();
                SelectList listItems = new SelectList(Departments, "Id", "Name");
                ViewBag.Departments = listItems;
            }
            if (!id.HasValue) return BadRequest();
            var employee = _unitOfWork.Employees.Get(id.Value);
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

