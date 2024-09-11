using Session03.BusinessLogicLayer.Interfaces;
using Session03.DataAccessLayer.Data;
using Session03.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session03.BusinessLogicLayer.repositories
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _dataContext;
        public EmployeeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public int Create(Employee entity)
        {
            _dataContext.Employees.Add(entity);
            return _dataContext.SaveChanges();
        }
        public int Delete(Employee entity)
        {
            _dataContext.Employees.Remove(entity);
            return _dataContext.SaveChanges();
        }

        public Employee? Get(int id)=> _dataContext.Employees.Find(id);
       

        public IEnumerable<Employee> GetAll()=> _dataContext.Employees.ToList();



        public int Update(Employee entity)
        {
            _dataContext.Employees.Update(entity);
            return _dataContext.SaveChanges();
        }
    }
}
