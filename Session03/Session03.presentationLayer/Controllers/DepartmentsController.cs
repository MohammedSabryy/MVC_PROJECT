using Microsoft.AspNetCore.Mvc;
using Session03.BusinessLogicLayer.repositories;
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

            if (!ModelState.IsValid) return View(model: department); _repository.Create(entity: department);

            return RedirectToAction(actionName: nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            var department = _repository.Get(id.Value);
            return View();
        }
    }
}
