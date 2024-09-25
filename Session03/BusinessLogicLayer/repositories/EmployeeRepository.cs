
namespace Session03.BusinessLogicLayer.repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context)
        {
        }

        public async Task <IEnumerable<Employee>> GetAllAsync(string Name)
            =>await  _dpSet.Where(e=>e.Name.ToLower().Contains(Name.ToLower())).Include(e => e.Department).ToListAsync();
        

        public async Task <IEnumerable<Employee>> GetAllWithDepartmentsAsync()=>
        await _dpSet.Include(e=>e.Department).ToListAsync();
    }
}
