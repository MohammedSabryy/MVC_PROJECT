using Session03.DataAccessLayer.Data;
using Session03.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session03.BusinessLogicLayer.repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _dataContext;
        public DepartmentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //private DataContext dataContext = new DataContext();


        public Department? Get(int id) => _dataContext.Departments.Find(id);
        public IEnumerable<Department> GetAll() => _dataContext.Departments.ToList();
        public int Create(Department entity)
        {
            _dataContext.Departments.Add(entity);
            return _dataContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dataContext.Departments.Update(entity);
            return _dataContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dataContext.Departments.Remove(entity);
            return _dataContext.SaveChanges();
        }

    }
}
