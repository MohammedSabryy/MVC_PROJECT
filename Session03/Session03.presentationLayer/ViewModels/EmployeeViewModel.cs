

namespace Session03.presentationLayer.ViewModels
{
    public class EmployeeViewModel
    {
        
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Address { get; set; }
            public decimal Salary { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public bool IsActive { get; set; }
            public Department? Department { get; set; }
            public int? DepartmentId { get; set; }
        
    }
}
