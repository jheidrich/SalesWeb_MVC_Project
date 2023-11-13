using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesProjectMVC.Data;
using SalesProjectMVC.Models;
using Microsoft.EntityFrameworkCore; 

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
            return _sellersContext.Seller.Include(reg=>reg.Department).FirstOrDefault(reg => reg.Id == id);
        }

        public void Remove(int id)
        {
            Seller obj = _sellersContext.Seller.Find(id);
            _sellersContext.Remove(obj);
            _sellersContext.SaveChanges(); 
        }
    }
}
