

namespace Session03.BusinessLogicLayer.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public IEnumerable<Employee> GetAll(string Name);
        public IEnumerable<Employee> GetAllWithDepartments();

    }
}
