using _10Days_OUT_CLDV.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using _10Days_OUT_CLDV.Services;

public class TransactionsController : Controller
{
    private readonly TableStorageService _tableStorageService;
    private readonly QueueService _queueService;

    public TransactionsController(TableStorageService tableStorageService, QueueService queueService)
    {
        _tableStorageService = tableStorageService;
        _queueService = queueService;
    }

    public async Task<IActionResult> Index()
    {
        var transactions = await _tableStorageService.GetAllTransactionsAsync();
        return View(transactions);
    }
    public async Task<IActionResult> Register()
    {
        var customers = await _tableStorageService.GetAllCustomersAsync();
        var products = await _tableStorageService.GetAllProductsAsync();

        if (customers == null || customers.Count == 0)
        {
            ModelState.AddModelError("", "No customers found. Please add some first.");
            return View(); 
        }

        if (products == null || products.Count == 0)
        {
            ModelState.AddModelError("", "No products found. Please add at least one item first.");
            return View();
        }

        ViewData["Customers"] = customers;
        ViewData["Products"] = products;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            transaction.Transaction_Date = DateTime.SpecifyKind(transaction.Transaction_Date, DateTimeKind.Utc);
            transaction.PartitionKey = "TransactionsPartition";
            transaction.RowKey = Guid.NewGuid().ToString();
            await _tableStorageService.AddTransactionAsync(transaction);
            //MessageQueue
            string message = $"New transaction by Customer {transaction.Transaction_Id} of Product {transaction.Product_ID} at {transaction.Transaction_Category} on {transaction.Transaction_Date}";
            await _queueService.SendMessageAsync(message);

            return RedirectToAction("Index");
        }
        else
        {
            foreach (var error in ModelState)
            {
                Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
            }
        }

        var customers = await _tableStorageService.GetAllCustomersAsync();
        var products = await _tableStorageService.GetAllProductsAsync();
        ViewData["Customers"] = customers;
        ViewData["Products"] = products;

        return View(transaction);
    }

}
