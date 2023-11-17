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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _sellersContext.Seller.ToListAsync(); 
        }

        public async Task InsertAsync(Seller sellerObj)
        {
            _sellersContext.Add(sellerObj);
            await _sellersContext.SaveChangesAsync(); 
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await 
                _sellersContext.Seller.Include(reg => reg.Department).FirstOrDefaultAsync(reg => reg.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            Seller obj = await _sellersContext.Seller.FindAsync(id);
            _sellersContext.Remove(obj);
            await _sellersContext.SaveChangesAsync(); 
        }

        public async Task UpdateAsync(Seller reg)
        {
            bool hasAnySeller = await _sellersContext.Seller.AnyAsync(obj => obj.Id == reg.Id); 
            if (!hasAnySeller)
                throw new NotFoundException("Object Id not Found!");

            if (reg.Name == null && reg.Department == null)
                reg = await FindByIdAsync(reg.Id); 

            try
            {
                _sellersContext.Update(reg);
                await _sellersContext.SaveChangesAsync(); 
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message); 
            }
        }
    }
}
