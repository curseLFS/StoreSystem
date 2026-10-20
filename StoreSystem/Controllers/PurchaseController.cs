using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreSystem.DAL.Data;
using StoreSystem.DAL.Models;
using static System.Collections.Specialized.BitVector32;

namespace StoreSystem.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly DBContext _context;

        public PurchaseController(DBContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            return View();
        }

        //GET: Purchase
        [HttpGet]
        public async Task<IActionResult> LoadPurchase()
        {
            var purchaseList = await _context.Purchases
                .Select(p => new Purchase
                {
                    Id = p.Id,
                    Item = new Item { ItemName = p.Item.ItemName },
                    ItemId = p.ItemId,
                    PurchaseDate = p.PurchaseDate,
                    Quantity = p.Quantity
                })
                .ToListAsync();

            return Json(new
            {
                draw = Request.Query["draw"],
                recordsTotal = purchaseList.Count,
                recordsFiltered = purchaseList.Count,
                data = purchaseList
            });
        }

        // GET: Purchase/Create
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

        // POST: Purchase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Purchase purchase)
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

                return View(purchase);
            }

            // Get selected item
            var item = await _context.Items
                .FirstOrDefaultAsync(i => i.Id == purchase.ItemId);
    
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

                return View(purchase);
            }

            _context.Purchases.Add(purchase);
           
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

