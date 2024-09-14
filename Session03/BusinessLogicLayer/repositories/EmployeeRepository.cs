namespace Session03.BusinessLogicLayer.repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context)
        {
        }

        public IEnumerable<Employee> GetAll(string Address)=> _dpSet.Where(e=>e.Address.ToLower()==Address.ToLower()).ToList();
        

        public IEnumerable<Employee> GetAllWithDepartments()=>
        _dpSet.Include(e=>e.Department).ToList();
    }
}
