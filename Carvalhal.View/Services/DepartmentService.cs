using Carvalhal.View.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carvalhal.View.Services
{
    public class DepartmentService
    {
        private readonly CarvalhalContext _context;

        public DepartmentService(CarvalhalContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(dp => dp.Name).ToListAsync();
        }
    }
}
