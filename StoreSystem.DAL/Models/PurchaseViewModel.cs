using Microsoft.AspNetCore.Mvc.Rendering;

namespace StoreSystem.DAL.Models
{
    public class PurchaseViewModel
    {
        public Purchase Purchase { get; set; } = new();

        public List<SelectListItem> Items { get; set; } = new();
    }
}
