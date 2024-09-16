namespace Session03.BusinessLogicLayer.repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context)
        {
        }

        public IEnumerable<Employee> GetAll(string Name)
            => _dpSet.Where(e=>e.Name.ToLower().Contains(Name.ToLower())).Include(e => e.Department).ToList();
        

        public IEnumerable<Employee> GetAllWithDepartments()=>
        _dpSet.Include(e=>e.Department).ToList();
    }
}
