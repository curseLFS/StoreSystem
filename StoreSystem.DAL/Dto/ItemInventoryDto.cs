using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.DAL.Dto
{
    public class ItemInventoryDto
    {
        public int Id { get; set; }

        public int ItemCode { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public decimal ItemPrice { get; set; }

        public int TotalPurchased { get; set; }

        public int TotalSold { get; set; }

        public int CurrentStock { get; set; }
    }
}
