
using StoreSystem.DAL.Dto;
using StoreSystem.DAL.Models;

namespace StoreSystem.DAL.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item?> GetItemAsync(int itemId);
        Task<Purchase> PurchaseAsync(Purchase purchase);
        Task<Sell> SellAsync(Sell sell);
        Task<int> GetCurrentStockAsync(int itemId);
        Task<IEnumerable<InventoryDto>> GetInventorySummaryAsync();
    }

}
