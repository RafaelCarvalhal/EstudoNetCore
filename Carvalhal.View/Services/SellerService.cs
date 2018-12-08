using Carvalhal.View.Models;
using Carvalhal.View.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carvalhal.View.Services
{
    public class SellerService
    {

        private readonly CarvalhalContext _context;

        public SellerService(CarvalhalContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            //return _context.Seller.Where(s => s.Id == id).SingleOrDefault();
            return await _context.Seller.Include(s => s.Department).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var s = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(s);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            if (!await _context.Seller.AnyAsync(s => s.Id == seller.Id))
            {
                throw new NotFoundException("Id Not Foud");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrenceException("Concurrence exception on update!");
            }
        }
    }
}
