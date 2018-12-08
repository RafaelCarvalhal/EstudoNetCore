using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carvalhal.View.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carvalhal.View.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesrsRecordService _salesrsRecordService;

        public SalesRecordsController(SalesrsRecordService salesrsRecordService)
        {
            _salesrsRecordService = salesrsRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesrsRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesrsRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
    }
}