using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SalesProjectMVC.Models; 

namespace SalesProjectMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> departmentsList = new List<Department>();

            departmentsList.Add(new Department { Id = 1, Name = "Eletronics" });
            departmentsList.Add(new Department { Id = 2, Name = "Fashion" }); 

            return View(departmentsList);
        }
    }
}
