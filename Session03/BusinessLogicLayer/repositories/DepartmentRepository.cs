



namespace Session03.BusinessLogicLayer.repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository 
    {
        public DepartmentRepository(DataContext context) : base(context)
        {
        }
    }
}
