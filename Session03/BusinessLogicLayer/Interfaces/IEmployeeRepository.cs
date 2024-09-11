using Session03.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session03.BusinessLogicLayer.Interfaces
{
    internal interface IEmployeeRepository
    {
        int Create(Employee entity);
        int Delete(Employee entity);
        Employee? Get(int id);
        IEnumerable<Employee> GetAll();
        int Update(Employee entity);
    }
}
