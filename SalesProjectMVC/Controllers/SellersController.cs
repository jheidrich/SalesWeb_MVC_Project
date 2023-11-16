﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SalesProjectMVC.Services;
using SalesProjectMVC.Models; 
using SalesProjectMVC.Models.ViewModel;
using SalesProjectMVC.Services.Exceptions; 

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

        public IActionResult Index()
        {
            List<Seller> sellerList = _sellerService.FindAll(); 
            return View(sellerList);
        }

        public IActionResult Create()
        {
            List<Department> departmentsList = _departmentService.FindAll(); 
            var viewModel = new SellerFormViewModel { Departments = departmentsList };
            return View(viewModel); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Seller obj = _sellerService.FindById(id.Value);

            if (obj == null)
                return NotFound();

            return View(obj); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            Seller obj = _sellerService.FindById(id.Value);

            if (obj == null)
                return NotFound();

            return View(obj); 
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Seller reg = _sellerService.FindById(id.Value);

            if (reg == null)
                return NotFound();

            List<Department> departmentList = _departmentService.FindAll();
            SellerFormViewModel sellerView = new SellerFormViewModel 
                { Seller = reg, Departments = departmentList };
            return View(sellerView); 
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller reg)
        {
            if (id != reg.Id)
                return BadRequest();

            try
            {
                _sellerService.Update(reg);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();  
            }
            catch (DbConcurrencyException)
            {
                return BadRequest(); 
            }
        }
    }
}
