using System;
using System.Diagnostics;
using Carvalhal.View.Models;
using Carvalhal.View.Models.ViewModels;
using Carvalhal.View.Services;
using Carvalhal.View.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Carvalhal.View.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController (SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel
            {
                Departments = departments
            };
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
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided!"});
            }
            var seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Seller not Found!" });
            }
            /*
            var viewModel = new SellerFormViewModel
            {
                Seller = seller
            };
            return View(viewModel);*/
            return View(seller);
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
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided!" });
            }
            var seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Seller not Found!" });
            }
            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided!" });
            }
            var seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Seller not Found!" });
            }

            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel
            {
                Seller = seller,
                Departments = departments
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                 
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            } catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { Message = ex.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var ViewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(ViewModel);
        }

    }
}