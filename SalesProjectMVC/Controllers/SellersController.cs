using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 

using SalesProjectMVC.Services;
using SalesProjectMVC.Models; 
using SalesProjectMVC.Models.ViewModel;
using SalesProjectMVC.Services.Exceptions;
using System.Diagnostics;

namespace SalesProjectMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellersService _sellerService;
        private readonly DepartmentService _departmentService; 

        public SellersController(SellersService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService; 
        }

        public async Task<IActionResult> Index()
        {
            List<Seller> sellerList = await _sellerService.FindAllAsync(); 
            return View(sellerList);
        }

        public async Task<IActionResult> Create()
        {
            List<Department> departmentsList = await _departmentService.FindAllAsync(); 
            var viewModel = new SellerFormViewModel { Departments = departmentsList };
            return View(viewModel); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departmentsList = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departmentsList };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index)); 
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" }); 

            Seller obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found!" }); 

            return View(obj); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index)); 
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });

            Seller obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found!" });

            return View(obj); 
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });

            Seller reg = await _sellerService.FindByIdAsync(id.Value);

            if (reg == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found!" });

            List<Department> departmentList = await _departmentService.FindAllAsync();
            SellerFormViewModel sellerView = new SellerFormViewModel 
                { Seller = reg, Departments = departmentList };
            return View(sellerView); 
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller reg)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departmentsList = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = reg, Departments = departmentsList };
                return View(viewModel); 
            }

            if (id != reg.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch!" });

            try
            {
                await _sellerService.UpdateAsync(reg);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
            catch (DbConcurrencyException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel); 
        }
    }
}
