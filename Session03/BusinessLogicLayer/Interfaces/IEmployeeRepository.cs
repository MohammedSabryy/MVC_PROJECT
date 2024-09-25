

namespace Session03.BusinessLogicLayer.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task <IEnumerable<Employee>> GetAllAsync(string Name);
        Task <IEnumerable<Employee>> GetAllWithDepartmentsAsync();

    }
}
