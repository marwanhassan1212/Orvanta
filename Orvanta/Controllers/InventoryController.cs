
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orvanta.Data;
using Orvanta.Service.InventoryStock;
using Orvanta.Services;
using Orvanta.ViewModels.Inventory;

namespace Orvanta.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;


        public InventoryController(IInventoryService inventoryService , IWarehouseService warehouseService ,
            IProductService productService, IUnitOfWork unitOfWork)
        {
            _inventoryService = inventoryService;
            _warehouseService = warehouseService;
            _productService = productService;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] InventoryStockFilterViewModel filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _inventoryService.GetStocksAsync(filter);
            return View(stocks);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int productId, int warehouseId)
        {
            if (productId <= 0 || warehouseId <= 0)
                return BadRequest();

            var stock = await _inventoryService.GetStockDetailsAsync(productId, warehouseId);

            if (stock == null)
                return NotFound();

            return View(stock);
        }

        [HttpGet]
        public async Task<IActionResult> Transactions([FromQuery] InventoryTransactionFilterViewModel filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hasProductFilter = filter.ProductId.HasValue && filter.ProductId.Value > 0;
            var hasWarehouseFilter = filter.WarehouseId.HasValue && filter.WarehouseId.Value > 0;

            if (!hasProductFilter && !hasWarehouseFilter)
            {
                ViewData["RequiresTxScope"] = true;
                return View();
            }

            var transactions = await _inventoryService.GetTransactionsAsync(filter);
            return View(transactions);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Accountant")]
        public IActionResult CreateOpeningBalance()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,Accountant")]
        public async Task<IActionResult> CreateOpeningBalance(int productId,
            int warehouseId, decimal quantity, decimal unitCost)

        {
            if (productId <= 0 || warehouseId <= 0)
                ModelState.AddModelError(string.Empty, "Invalid product or warehouse.");

            if (quantity <= 0)
                ModelState.AddModelError(nameof(quantity), "Quantity must be greater than zero.");


            if (unitCost < 0)
                ModelState.AddModelError(nameof(unitCost), "Unit cost cannot be negative.");


            if (!ModelState.IsValid)
                return View();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                return Forbid();

            var result = await _inventoryService.CreateOpeningBalanceAsync(productId, warehouseId,
                quantity, unitCost, userId);

            if (!result)
            {
                ModelState.AddModelError(string.Empty,
                    "Unable to create opening balance. Please verify the product, warehouse, and existing inventory transactions.");

                return View();
            }

            TempData["SuccessMessage"] = "Opening balance created successfully.";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> CheckAvailability(int productId,
            int warehouseId, decimal requestedQuantity)
        {
            if (productId <= 0 || warehouseId <= 0 || requestedQuantity <= 0)
                return BadRequest();

            var available = await _inventoryService.IsStockAvailableAsync(productId, warehouseId, requestedQuantity);
            var availableQuantity = await _inventoryService.GetAvailableQuantityAsync(productId, warehouseId);

            return Json(new
            {
                IsAvailable = available,
                AvailableQuantity = availableQuantity,
                RequestedQuantity = requestedQuantity
            });
        }

        [HttpGet]
        public async Task<IActionResult> SearchProducts(string? term)
        {
            var items = await _productService.GetFilteredAsync(new Orvanta.ViewModels.Products.ProductFilterViewModel
            {
                SearchTerm = term,
                IsActive = true,
                PageNumber = 1,
                PageSize = 20
            });

            var data = new List<object>();
            if (items?.Items != null)
            {
                foreach (var p in (System.Collections.IEnumerable)items.Items)
                {
                    if (p == null) continue;

                    var type = p.GetType();
                    var idProp = type.GetProperty("Id");
                    var nameProp = type.GetProperty("Name");
                    if (idProp == null || nameProp == null) continue;

                    var id = Convert.ToInt32(idProp.GetValue(p));
                    var name = Convert.ToString(nameProp.GetValue(p)) ?? string.Empty;

                    decimal purchasePrice = 0m;
                    var details = await _productService.GetByIdAsync(id);
                    if (details != null)
                    {
                        var dType = details.GetType();
                        var priceProp = dType.GetProperty("PurchasePrice")
                                        ?? dType.GetProperty("CostPrice")
                                        ?? dType.GetProperty("UnitCost")
                                        ?? dType.GetProperty("Price");

                        if (priceProp != null)
                        {
                            var rawPrice = priceProp.GetValue(details);
                            if (rawPrice != null && decimal.TryParse(rawPrice.ToString(), out var parsed))
                            {
                                purchasePrice = parsed;
                            }
                        }
                    }

                    data.Add(new
                    {
                        id,
                        name,
                        purchasePrice
                    });
                }
            }

            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> SearchWarehouses(string? term)
        {
            var items = await _warehouseService.GetFilteredAsync(new Orvanta.ViewModels.Warehouses.WarehouseFilterViewModel
            {
                SearchTerm = term,
                IsActive = true,
                PageNumber = 1,
                PageSize = 20
            });

            var data = ((System.Collections.IEnumerable)items.Items).Cast<dynamic>()
                .Select(w => new { id = (int)w.Id, name = (string)w.Name });
            return Json(data);
        }
    }
}
