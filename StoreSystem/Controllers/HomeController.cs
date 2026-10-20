using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreSystem.DAL.Data;
using StoreSystem.DAL.Dto;
using StoreSystem.DAL.Models;
using StoreSystem.Models;
using System.Diagnostics;
using System.Text.Json;

namespace StoreSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext _context;

        public HomeController(DBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadDashBoard()
        {
            var vm = new DashboardViewModel
            {
                TotalItems = await _context.Items.CountAsync(),

                TotalPurchased = await _context.Purchases
             .SumAsync(x => (int?)x.Quantity) ?? 0,

                TotalSold = await _context.Sells
             .SumAsync(x => (int?)x.Quantity) ?? 0,

                Inventory = await _context.Items
                 .Select(i => new ItemInventoryDto
                 {
                     Id = i.Id,
                     ItemCode = i.ItemCode,
                     ItemName = i.ItemName
                 })
                 .ToListAsync()
                };

            vm.CurrentStock = vm.TotalPurchased - vm.TotalSold;

            return Json(new
            {
                draw = Request.Query["draw"],
                recordsTotal = vm.Inventory.Count,
                recordsFiltered = vm.Inventory.Count,
                data = vm.Inventory,
                totalItems = vm.TotalItems,
                totalPurchased = vm.TotalPurchased,
                totalSold = vm.TotalSold,
                currentStock = vm.CurrentStock
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetItemSalesChart()
        {
            var data = await _context.Items
                .Select(i => new
                {
                    ItemName = i.ItemName,

                    Purchased = i.Purchase
                        .Sum(p => (int?)p.Quantity) ?? 0,

                    Sold = i.Sell
                        .Sum(s => (int?)s.Quantity) ?? 0
                })
                .ToListAsync();

            return Json(data);
        }
    }
}
