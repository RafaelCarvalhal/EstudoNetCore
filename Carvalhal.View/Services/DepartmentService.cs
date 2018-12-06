using Carvalhal.View.Models;
using System.Collections.Generic;
using System.Linq;

namespace Carvalhal.View.Services
{
    public class DepartmentService
    {
        private readonly CarvalhalContext _context;

        public DepartmentService(CarvalhalContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(dp => dp.Name).ToList();
        }
    }
}
