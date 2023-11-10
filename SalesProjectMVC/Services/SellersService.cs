using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesProjectMVC.Data;
using SalesProjectMVC.Models; 

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
            if (sellerObj.Department == null)
                sellerObj.Department = _sellersContext.Department.First(); 

            _sellersContext.Add(sellerObj);
            _sellersContext.SaveChanges(); 
        }
    }
}
