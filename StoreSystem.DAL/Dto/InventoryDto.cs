using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.DAL.Dto
{
    public class InventoryDto
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public int TotalPurchased { get; set; }

        public int TotalSold { get; set; }

        public int CurrentStock { get; set; }
    }
}
