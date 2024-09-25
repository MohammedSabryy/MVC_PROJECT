
namespace Session03.presentationLayer.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchValue)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrWhiteSpace(SearchValue))
                employees = await _unitOfWork.Employees.GetAllWithDepartmentsAsync();
            else employees = await _unitOfWork.Employees.GetAllAsync(SearchValue);
            var employeeVM = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(employeeVM); 
            
        }

        public async Task<IActionResult> Create()
        {
            var Departments =await _unitOfWork.Departments.GetAllAsync();
            SelectList listItems = new SelectList(Departments,"Id","Name");
            ViewBag.Departments = listItems;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {


            // Server Side Validation
            if (!ModelState.IsValid) return View(model: employeeVM);
            if(employeeVM.Image is not null)
                employeeVM.ImageName = await DocumentSettings.UploadFileAsync(employeeVM.Image, "Images");
            var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            await _unitOfWork.Employees.AddAsync(entity: employee);
            await _unitOfWork.saveChangesAsync();
            return RedirectToAction(actionName: nameof(Index));
        }
        public async Task<IActionResult> Details(int? id) =>await EmployeeControllerHandler(id, nameof(Details));

        public async Task<IActionResult> Edit(int? id) =>await EmployeeControllerHandler(id, nameof(Edit));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeeVM.Image is not null)
                        employeeVM.ImageName =await  DocumentSettings.UploadFileAsync(employeeVM.Image, "Images");
                    var employee = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                    _unitOfWork.Employees.Update(employee);
                    if (await _unitOfWork.saveChangesAsync() > 0)
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
        public async Task<IActionResult> Delete(int? id) =>await EmployeeControllerHandler(id, nameof(Delete));

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                _unitOfWork.Employees.Delete(employee);
                if(await _unitOfWork.saveChangesAsync() >0 && employee.ImageName is not null)
                {
                    DocumentSettings.DeleteFile("Images", employee.ImageName);
                }
                return RedirectToAction(nameof(Index));


            }
            return View(employee);
        }
        private async Task<IActionResult> EmployeeControllerHandler(int? id, string viewName)
        {
            if (viewName == nameof(Edit))
            {
                var Departments =await _unitOfWork.Departments.GetAllAsync();
                SelectList listItems = new SelectList(Departments, "Id", "Name");
                ViewBag.Departments = listItems;
            }
            if (!id.HasValue) return BadRequest();
            var employee =await _unitOfWork.Employees.GetAsync(id.Value);
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

