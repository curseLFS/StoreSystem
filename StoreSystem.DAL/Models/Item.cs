using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreSystem.DAL.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Item Code must be greater than 0.")]
        public int ItemCode { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? ItemName { get; set; }
                
        [Range(0.01, 999999999.99, ErrorMessage = "Item Price must be greater than 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemPrice { get; set; }

        public string? ItemDescription { get; set; }
        public ICollection<Purchase> Purchase { get; set; } = new List<Purchase>();

        public ICollection<Sell> Sell { get; set; } = new List<Sell>();

        [NotMapped]
        public int CurrentStock => Purchase.Sum(p => p.Quantity) - Sell.Sum(s => s.Quantity);
        //public int CurrentStock { get; set; }
    }
}
