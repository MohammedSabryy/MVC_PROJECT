using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session03.BusinessLogicLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository Employees { get; }
        public IDepartmentRepository Departments { get; }
        public int saveChanges();

    }
}
