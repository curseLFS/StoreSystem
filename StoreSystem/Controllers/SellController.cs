using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreSystem.DAL.Data;
using StoreSystem.DAL.Models;

namespace StoreSystem.Controllers
{
    public class SellController : Controller
    {
        private readonly DBContext _context;

        public SellController(DBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        //GET: Sell
        [HttpGet]
        public async Task<IActionResult> LoadSell() 
        {
            var sellList = await _context.Sells
                .Select(s => new Sell
                {
                    Id = s.Id,
                    Item = new Item { ItemName = s.Item.ItemName },
                    ItemId = s.ItemId,
                    Quantity = s.Quantity,
                    Remarks = s.Remarks,
                    SellDate = s.SellDate,
                    SellingPrice = s.SellingPrice
                })
                .ToListAsync();

            return Json(new
            {
                draw = Request.Query["draw"],
                recordsTotal = sellList.Count,
                recordsFiltered = sellList.Count,
                data = sellList
            });
        }

        // GET: Sell/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Items = _context.Items
            .Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = $"{i.ItemCode} - {i.ItemName}"
            })
            .ToList();

            return View();
        }

        // POST: Sell/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sell sell)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Items = _context.Items
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = $"{i.ItemCode} - {i.ItemName}"
                })
                .ToList();

                return View(sell);
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(i => i.Id == sell.ItemId);

            if (item == null)
            {
                ModelState.AddModelError("", "Item not found.");

                ViewBag.Items = _context.Items
                    .Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = $"{i.ItemCode} - {i.ItemName}"
                    })
                    .ToList();

                return View(sell);
            }

            _context.Sells.Add(sell);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetItemPrice(int itemId)
        {
            var item = await _context.Items
                .FirstOrDefaultAsync(i => i.Id == itemId);

            if (item == null)
                return NotFound();

            return Json(item.ItemPrice);
        }

        [HttpGet]
        public async Task<IActionResult> GetQuantity(int itemId)
        {
            var item = await _context.Items
                .Include(i => i.Purchase)
                .Include(i => i.Sell)
                .FirstOrDefaultAsync(i => i.Id == itemId);

            if (item == null)
                return NotFound();

            var quantity =
                item.Purchase.Sum(p => p.Quantity) -
                item.Sell.Sum(s => s.Quantity);

            return Json(quantity);
        }
    }
}
