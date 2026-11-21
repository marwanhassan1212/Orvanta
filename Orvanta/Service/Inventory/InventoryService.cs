
using Microsoft.EntityFrameworkCore;
using Orvanta.Data;
using Orvanta.Models;
using Orvanta.Repositories.Interfaces;
using Orvanta.Service.InventoryStock;
using Orvanta.ViewModels.Common;
using Orvanta.ViewModels.Inventory;

namespace Orvanta.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<InventoryStockListViewModel>> GetStocksAsync(InventoryStockFilterViewModel filter)
        {
            filter ??= new InventoryStockFilterViewModel();
            IEnumerable<InventoryStock> stocks;

            if (filter.WarehouseId.HasValue)
            {
                stocks = await _unitOfWork.InventoryStocks.GetByWarehouseIdAsync(filter.WarehouseId.Value);

            }
            else if (filter.ProductId.HasValue)
            {
                stocks = await _unitOfWork.InventoryStocks.GetByProductIdAsync(filter.ProductId.Value);

            }
            else
            {
                stocks = await _unitOfWork.InventoryStocks.GetAllAsync();
            }

            var query = stocks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var term = filter.SearchTerm.Trim();

                query = query.Where(x =>
                    x.Product.Name.Contains(term) ||
                    x.Warehouse.Name.Contains(term) ||
                    x.Warehouse.Code.Contains(term));
            }

            query = filter.SortBy?.ToLower() switch
            {
                "quantity" => filter.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Quantity)
                    : query.OrderBy(x => x.Quantity),

                "averagecost" => filter.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.AverageCost)
                    : query.OrderBy(x => x.AverageCost),

                "warehouse" => filter.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Warehouse.Name)
                    : query.OrderBy(x => x.Warehouse.Name),

                _ => filter.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Product.Name)
                    : query.OrderBy(x => x.Product.Name)
            };

            var pageNumber = Math.Max(1, filter.PageNumber);
            var pageSize = filter.PageSize is 10 or 20 or 50
                ? filter.PageSize
                : 10;

            var totalCount = query.Count();

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new InventoryStockListViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    SKU = x.Product.SKU,
                    WarehouseId = x.WarehouseId,
                    WarehouseName = x.Warehouse.Name,
                    Quantity = x.Quantity,
                    AverageCost = x.AverageCost,
                    UpdatedAt = x.UpdatedAt
                })
                .ToList();

            return new PagedResult<InventoryStockListViewModel>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }


        public async Task<InventoryStockDetailsViewModel?> GetStockDetailsAsync(int productId, int warehouseId)
        {
            var stock = await _unitOfWork.InventoryStocks.GetByProductAndWarehouseAsync(productId, warehouseId);
            if (stock == null)
                return null;

            return new InventoryStockDetailsViewModel
            {
                Id = stock.Id,
                ProductId = stock.ProductId,
                ProductName = stock.Product.Name,
                SKU = stock.Product.SKU,
                WarehouseId = stock.WarehouseId,
                WarehouseName = stock.Warehouse.Name,
                Quantity = stock.Quantity,
                AverageCost = stock.AverageCost,
                UpdatedAt = stock.UpdatedAt
            };
        }


        public async Task<PagedResult<InventoryTransactionListViewModel>>
            GetTransactionsAsync(InventoryTransactionFilterViewModel filter)
        {
            filter ??= new InventoryTransactionFilterViewModel();

            IEnumerable<InventoryTransaction> transactions;

            if (filter.ProductId.HasValue && filter.WarehouseId.HasValue)
            {
                transactions = await _unitOfWork.Transactions
                    .GetByProductAndWarehouseAsync(
                        filter.ProductId.Value,
                        filter.WarehouseId.Value);
            }
            else if (filter.ProductId.HasValue)
            {
                transactions = await _unitOfWork.Transactions
                    .GetByProductIdAsync(filter.ProductId.Value);
            }
            else if (filter.WarehouseId.HasValue)
            {
                transactions = await _unitOfWork.Transactions
                    .GetByWarehouseIdAsync(filter.WarehouseId.Value);
            }
            else
            {
                throw new InvalidOperationException("A ProductId or WarehouseId filter is required.");

            }

            var query = transactions.AsQueryable();

            if (filter.TransactionType.HasValue)
            {
                query = query.Where(x => x.TransactionType == filter.TransactionType.Value);
            }

            if (filter.FromDate.HasValue)
            {
                query = query.Where(x => x.CreatedAt >= filter.FromDate.Value);

            }

            if (filter.ToDate.HasValue)
            {
                var endDate = filter.ToDate.Value.Date.AddDays(1);
                query = query.Where(x => x.CreatedAt < endDate);
            }

            query = filter.SortDirection?.ToLower() == "asc"
                ? query.OrderBy(x => x.CreatedAt).ThenBy(x => x.Id)
                : query.OrderByDescending(x => x.CreatedAt)
                    .ThenByDescending(x => x.Id);

            var pageNumber = Math.Max(1, filter.PageNumber);
            var pageSize = filter.PageSize is 10 or 20 or 50 ? filter.PageSize : 10;

            var totalCount = query.Count();

            var items = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new InventoryTransactionListViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    SKU = x.Product.SKU,
                    WarehouseId = x.WarehouseId,
                    WarehouseName = x.Warehouse.Name,
                    TransactionType = x.TransactionType,
                    Quantity = x.Quantity,
                    UnitCost = x.UnitCost,
                    TotalCost = x.TotalCost,
                    CreatedByUserId = x.CreatedByUserId,
                    CreatedAt = x.CreatedAt,
                    PurchaseInvoiceItemId = x.PurchaseInvoiceItemId,
                    SalesInvoiceItemId = x.SalesInvoiceItemId
                })
                .ToList();

            return new PagedResult<InventoryTransactionListViewModel>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<bool> CreateOpeningBalanceAsync(int productId,
            int warehouseId, decimal quantity, decimal unitCost, string createdByUserId)
        {
            if (quantity <= 0 || unitCost < 0 ||
                string.IsNullOrWhiteSpace(createdByUserId))
                return false;

            var existingStock = await _unitOfWork.InventoryStocks
                .GetByProductAndWarehouseAsync(productId, warehouseId);

            if (existingStock != null)
                return false;

            var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var stock = new InventoryStock
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    Quantity = quantity,
                    AverageCost = unitCost,
                    UpdatedAt = DateTime.UtcNow
                };

                var inventoryTransaction = new InventoryTransaction
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    TransactionType = InventoryTransactionType.OpeningBalance,
                    Quantity = quantity,
                    UnitCost = unitCost,
                    TotalCost = Math.Round(quantity * unitCost, 2, MidpointRounding.AwayFromZero),
                    CreatedByUserId = createdByUserId,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.InventoryStocks.AddAsync(stock);
                await _unitOfWork.Transactions.AddAsync(
                    inventoryTransaction);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<bool> IsStockAvailableAsync(int productId,
            int warehouseId, decimal requestedQuantity)
        {
            if (requestedQuantity <= 0)
                return false;

            var stock = await _unitOfWork.InventoryStocks.GetByProductAndWarehouseAsync(productId, warehouseId);
            return stock != null && stock.Quantity >= requestedQuantity;
        }

        public async Task<decimal> GetAvailableQuantityAsync(int productId, int warehouseId)
        {
            var stock = await _unitOfWork.InventoryStocks.GetByProductAndWarehouseAsync(productId, warehouseId);
            return stock?.Quantity ?? 0;
        }


        public async Task<bool> ProcessPurchaseInAsync(int productId,
            int warehouseId, decimal quantity, decimal unitCost,
            int purchaseInvoiceItemId, string createdByUserId)
        {
            if (quantity <= 0 || unitCost < 0 ||
                purchaseInvoiceItemId <= 0 ||
                string.IsNullOrWhiteSpace(createdByUserId))
                return false;

            if (await _unitOfWork.Transactions.ExistsForPurchaseInvoiceItemAsync(purchaseInvoiceItemId))
                return false;

            var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var stock = await _unitOfWork.InventoryStocks.GetByProductAndWarehouseAsync(productId, warehouseId);
                if (stock == null)
                {
                    stock = new InventoryStock
                    {
                        ProductId = productId,
                        WarehouseId = warehouseId,
                        Quantity = 0,
                        AverageCost = 0,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _unitOfWork.InventoryStocks.AddAsync(stock);
                }

                var oldValue = stock.Quantity * stock.AverageCost;
                var incomingValue = quantity * unitCost;
                var newQuantity = stock.Quantity + quantity;
                var newAverageCost = newQuantity == 0 ? 0 : (oldValue + incomingValue) / newQuantity;

                stock.Quantity = newQuantity;
                stock.AverageCost = Math.Round(newAverageCost, 4, MidpointRounding.AwayFromZero);
                stock.UpdatedAt = DateTime.UtcNow;

                if (stock.Id != 0)
                    _unitOfWork.InventoryStocks.Update(stock);

                var inventoryTransaction = new InventoryTransaction
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    TransactionType = InventoryTransactionType.PurchaseIn,
                    Quantity = quantity,
                    UnitCost = unitCost,
                    TotalCost = Math.Round(incomingValue, 2, MidpointRounding.AwayFromZero),
                    PurchaseInvoiceItemId = purchaseInvoiceItemId,
                    CreatedByUserId = createdByUserId,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Transactions.AddAsync(
                    inventoryTransaction);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }


        public async Task<bool> ProcessSaleOutAsync(int productId, int warehouseId,
            decimal quantity, int salesInvoiceItemId, string createdByUserId)
        {
            if (quantity <= 0 ||
                salesInvoiceItemId <= 0 ||
                string.IsNullOrWhiteSpace(createdByUserId))
                return false;

            if (await _unitOfWork.Transactions
                .ExistsForSalesInvoiceItemAsync(salesInvoiceItemId))
                return false;

            var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var stock = await _unitOfWork.InventoryStocks.GetByProductAndWarehouseAsync(productId, warehouseId);

                if (stock == null || stock.Quantity < quantity)
                    return false;

                var unitCost = stock.AverageCost;
                var totalCost = Math.Round(quantity * unitCost, 2, MidpointRounding.AwayFromZero);

                stock.Quantity -= quantity;
                stock.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.InventoryStocks.Update(stock);

                var inventoryTransaction = new InventoryTransaction
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    TransactionType = InventoryTransactionType.SaleOut,
                    Quantity = quantity,
                    UnitCost = unitCost,
                    TotalCost = totalCost,
                    SalesInvoiceItemId = salesInvoiceItemId,
                    CreatedByUserId = createdByUserId,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Transactions.AddAsync(
                    inventoryTransaction);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
