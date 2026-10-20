using StoreSystem.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.DAL.Models
{
    public class DashboardViewModel
    {
        public int TotalItems { get; set; }

        public int TotalPurchased { get; set; }

        public int TotalSold { get; set; }

        public int CurrentStock { get; set; }

        public decimal InventoryValue { get; set; }

        public List<Purchase> RecentPurchases { get; set; } = new();

        public List<Sell> RecentSales { get; set; } = new();
        public List<ItemInventoryDto> Inventory { get; set; } = new();
    }
}
