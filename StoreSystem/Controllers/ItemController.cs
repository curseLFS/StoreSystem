using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreSystem.DAL.Data;
using StoreSystem.DAL.Dto;
using StoreSystem.DAL.Models;

namespace StoreSystem.Controllers
{
    public class ItemController : Controller
    {
        private readonly DBContext _context;

        public ItemController(DBContext context)
        {
            _context = context;
        }

        // GET: Item/Create
        [HttpGet]
        public IActionResult Create()
        {
            int nextCode = 10001;

            var lastItem = _context.Items
                .OrderByDescending(i => i.ItemCode)
                .FirstOrDefault();

            if (lastItem != null)
                nextCode = lastItem.ItemCode + 1;

            var item = new Item
            {
                ItemCode = nextCode
            };

            return View(item);
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item item)
        {
            if (_context.Items.Any(x => x.ItemCode == item.ItemCode))
            {
                item.ItemCode = _context.Items.Max(x => x.ItemCode) + 1;

                ModelState.AddModelError(
                    "ItemCode", 
                    $"Item Code already exists. Suggested code: {item.ItemCode}");
            }

            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                _context.SaveChanges();

                TempData["Success"] = "Item created successfully.";

                return RedirectToAction(nameof(Create));
            }

            return View(item);
        }

        // GET: Item
        public IActionResult Index()
        {
            var inventory = _context.Items
                .Select(i => new ItemInventoryDto
                {
                    Id = i.Id,
                    ItemCode = i.ItemCode,
                    ItemName = i.ItemName,
                    ItemPrice = i.ItemPrice,

                    TotalPurchased = _context.Purchases
                        .Where(p => p.ItemId == i.Id)
                        .Sum(p => (int?)p.Quantity) ?? 0,

                    TotalSold = _context.Sells
                        .Where(s => s.ItemId == i.Id)
                        .Sum(s => (int?)s.Quantity) ?? 0
                })
                .ToList();

            foreach (var item in inventory)
            {
                item.CurrentStock =
                    item.TotalPurchased -
                    item.TotalSold;
            }

            return View(inventory);
        }

        // POST: Item/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Item item)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Items.Update(item);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Item updated successfully";

            return RedirectToAction(nameof(Index));
        }

        // POST: Item/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items
                .Include(i => i.Purchase)
                .Include(i => i.Sell)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                TempData["Error"] = "Item not found.";
                return RedirectToAction(nameof(Index));
            }

            if (item.Purchase.Any() || item.Sell.Any())
            {
                TempData["Error"] =
                    "Cannot delete item because it already has transactions.";
                return RedirectToAction(nameof(Index));
            }

            _context.Items.Remove(item);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Item deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}

