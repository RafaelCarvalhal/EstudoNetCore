using Carvalhal.View.Models;
using Carvalhal.View.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Carvalhal.View.Services
{
    public class SellerService
    {

        private readonly CarvalhalContext _context;

        public SellerService(CarvalhalContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            //return _context.Seller.Where(s => s.Id == id).SingleOrDefault();
            return _context.Seller.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
        }

        public void Remove(int id)
        {
            //var s = FindById(id);
            var s = _context.Seller.Find(id);
            _context.Seller.Remove(s);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if (!_context.Seller.Any(s => s.Id == seller.Id))
            {
                throw new NotFoundException("Id Not Foud");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            } catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrenceException("Concurrence exception on update!");
            }
        }
    }
}
