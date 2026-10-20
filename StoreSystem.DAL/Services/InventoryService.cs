using Microsoft.EntityFrameworkCore;
using StoreSystem.DAL.Data;
using StoreSystem.DAL.Dto;
using StoreSystem.DAL.Interfaces;
using StoreSystem.DAL.Models;

namespace StoreSystem.DAL.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly DBContext _context;

        public InventoryService(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _context.Items
                .Include(i => i.Purchase)
                .Include(i => i.Sell)
                .ToListAsync();
        }

        public async Task<Item?> GetItemAsync(int itemId)
        {
            return await _context.Items
                .Include(i => i.Purchase)
                .Include(i => i.Sell)
                .FirstOrDefaultAsync(i => i.Id == itemId);
        }

        public async Task<Purchase> PurchaseAsync(Purchase purchase)
        {
            var itemExists = await _context.Items
                .AnyAsync(i => i.Id == purchase.ItemId);

            if (!itemExists)
                throw new Exception("Item not found.");

            _context.Purchases.Add(purchase);

            await _context.SaveChangesAsync();

            return purchase;
        }

        public async Task<Sell> SellAsync(Sell sell)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var stock = await GetCurrentStockAsync(sell.ItemId);

            if (stock < sell.Quantity)
            {
                throw new InvalidOperationException(
                $"Insufficient stock. Available stock: {stock}");
            }

            _context.Sells.Add(sell);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return sell;
        }

        public async Task<int> GetCurrentStockAsync(int itemId)
        {
            var purchased = await _context.Purchases
                .Where(p => p.ItemId == itemId)
                .SumAsync(p => (int?)p.Quantity) ?? 0;

            var sold = await _context.Sells
                .Where(s => s.ItemId == itemId)
                .SumAsync(s => (int?)s.Quantity) ?? 0;

            return purchased - sold;
        }

        public async Task<IEnumerable<InventoryDto>> GetInventorySummaryAsync()
        {
            return await _context.Items
                .Select(i => new InventoryDto
                {
                    ItemId = i.Id,
                    ItemName = i.ItemName,
                    TotalPurchased = i.Purchase.Sum(p => p.Quantity),
                    TotalSold = i.Sell.Sum(s => s.Quantity),
                    CurrentStock =
                i.Purchase.Sum(p => p.Quantity)
                - i.Sell.Sum(s => s.Quantity)
                })
                .ToListAsync();
        }
    }

}
