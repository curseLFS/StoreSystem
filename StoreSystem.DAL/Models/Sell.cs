using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreSystem.DAL.Models
{
    public class Sell
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }

        public Item? Item { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime SellDate { get; set; }

        [Required]
        [Range(0.01, 999999999.99, ErrorMessage = "Selling price must be greater than 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SellingPrice { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }
    }
}
