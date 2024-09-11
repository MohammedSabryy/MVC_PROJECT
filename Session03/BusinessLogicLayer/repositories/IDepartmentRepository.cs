using Session03.DataAccessLayer.Models;

namespace Session03.BusinessLogicLayer.repositories
{
    public interface IDepartmentRepository
    {
        int Create(Department entity);
        int Delete(Department entity);
        Department? Get(int id);
        IEnumerable<Department> GetAll();
        int Update(Department entity);
    }
}