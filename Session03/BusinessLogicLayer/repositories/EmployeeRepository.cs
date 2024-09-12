




namespace Session03.BusinessLogicLayer.repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context)
        {
        }

        public IEnumerable<Employee> GetAll(string Address)
        {
           return _dpSet.Where(e=>e.Address.ToLower()==Address.ToLower()).ToList();
        }
    }
}
