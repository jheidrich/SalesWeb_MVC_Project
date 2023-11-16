using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SalesProjectMVC.Data;
using SalesProjectMVC.Models;
using SalesProjectMVC.Services.Exceptions;

namespace SalesProjectMVC.Services
{
    public class SellersService
    {
        private readonly SalesProjectMVCContext _sellersContext;

        public SellersService(SalesProjectMVCContext sellersContext)
        {
            _sellersContext = sellersContext;
        }

        public List<Seller> FindAll()
        {
            return _sellersContext.Seller.ToList(); 
        }

        public void Insert(Seller sellerObj)
        {
            _sellersContext.Add(sellerObj);
            _sellersContext.SaveChanges(); 
        }

        public Seller FindById(int id)
        {
            return _sellersContext.Seller.Include(reg => reg.Department).FirstOrDefault(reg => reg.Id == id);
        }

        public void Remove(int id)
        {
            Seller obj = _sellersContext.Seller.Find(id);
            _sellersContext.Remove(obj);
            _sellersContext.SaveChanges(); 
        }

        public void Update(Seller reg)
        {
            if (!_sellersContext.Seller.Any(obj => obj.Id == reg.Id))
                throw new NotFoundException("Object Id not Found!");

            if (reg.Name == null && reg.Department == null)
                reg = FindById(reg.Id); 

            try
            {
                _sellersContext.Update(reg);
                _sellersContext.SaveChanges(); 
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message); 
            }
        }
    }
}
