

namespace Session03.BusinessLogicLayer.Interfaces
{
    internal interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public IEnumerable<Employee> GetAll(string Address);
    }
}
