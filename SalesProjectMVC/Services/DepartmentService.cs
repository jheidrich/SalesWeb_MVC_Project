using System;
using System.Linq;
using System.Collections.Generic; 
using SalesProjectMVC.Data;
using SalesProjectMVC.Models; 

namespace SalesProjectMVC.Services
{
    public class DepartmentService
    {
        private SalesProjectMVCContext _departmentContext;

        public DepartmentService(SalesProjectMVCContext departmentContext)
        {
            _departmentContext = departmentContext;
        }

        public List<Department> FindAll()
        {
            return _departmentContext.Department.OrderBy(dep => dep.Name).ToList(); 
        }

        public Department FindById(int id)
        {
            return _departmentContext.Department.FirstOrDefault(reg => reg.Id == id); 
        }
    }
}
