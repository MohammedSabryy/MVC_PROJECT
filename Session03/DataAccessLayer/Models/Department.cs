namespace Session03.DataAccessLayer.Models
{
    public class Department
    {
        [Range(0, 500)]
        public int Id { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Is Required !!")]
        public string Name { get; set; }
        [Display(Name = "Created At")]
        public DateTime Date { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
