using Microsoft.AspNetCore.Mvc;
using StoreSystem.DAL.Interfaces;
using StoreSystem.DAL.Models;

namespace StoreSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }


        [HttpPost("purchase")]
        public async Task<IActionResult> Purchase(Purchase purchase)
        {
            var result = await _service.PurchaseAsync(purchase);

            return Ok(result);
        }

        [HttpPost("sell")]
        public async Task<IActionResult> Sell(Sell sell)
        {
            var result = await _service.SellAsync(sell);

            return Ok(result);
        }

        [HttpGet("stock/{itemId}")]
        public async Task<IActionResult> GetStock(int itemId)
        {
            var stock = await _service
            .GetCurrentStockAsync(itemId);

            return Ok(stock);
        }
    }
}
