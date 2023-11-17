using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 

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

        public async Task<List<Department>> FindAllAsync()
        {
            return await _departmentContext.Department.OrderBy(dep => dep.Name).ToListAsync(); 
        }

        public async Task<Department> FindByIdAsync(int id)
        {
            return await _departmentContext.Department.FirstOrDefaultAsync(reg => reg.Id == id); 
        }
    }
}
