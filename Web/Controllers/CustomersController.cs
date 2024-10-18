using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System.Runtime.InteropServices.JavaScript;
namespace Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AppDbcontext _dbcontext;

        public CustomersController(AppDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customer = await _dbcontext.Customers.ToListAsync();
            return View(customer);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Customers.Add(customer);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public IActionResult Details(int id)
        {
            var cus = _dbcontext.Customers.Find(id);

            if(cus == null)
            {
                return NotFound();
            }
            return View(cus);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            if(id == null) { return View("Error"); }
            var customer = _dbcontext.Customers.Find(id); // Find thường nên dùng cho khóa chính 
            if (customer == null) 
            {
                return NotFound();
            }
            return View(customer);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if(id != customer.CustomerId)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid) // cái này để làm gì 
                {
                    var updatecudtomer = await _dbcontext.Customers.FindAsync(id);

                    if (updatecudtomer != null)
                    {
                        updatecudtomer.CustomerId = customer.CustomerId;
                        updatecudtomer.FullName = customer.FullName;
                        updatecudtomer.Email = customer.Email;
                        updatecudtomer.PhoneNumber = customer.PhoneNumber;

                        await _dbcontext.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Khong the luu customer. ");
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var cus = _dbcontext.Customers.Find(id);
            if(cus == null)
            {
                return NotFound(0);
            }
            return View(cus); 
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cus = await _dbcontext.Customers.FindAsync(id);

            if (cus != null)
            {
                // Lấy danh sách khách hàng đã xóa từ Session
                var deletedCustomersJson = HttpContext.Session.GetString("DeletedCustomers");
                var deletedCustomers = string.IsNullOrEmpty(deletedCustomersJson)
                    ? new List<Customer>()
                    : JsonConvert.DeserializeObject<List<Customer>>(deletedCustomersJson);

                // Thêm khách hàng bị xóa vào danh sách
                deletedCustomers.Add(cus);

                // Cập nhật lại session với danh sách khách hàng mới
                HttpContext.Session.SetString("DeletedCustomers", JsonConvert.SerializeObject(deletedCustomers));

                

                // Kiểm tra xem có bao nhiêu đối tượng trong session
                Console.WriteLine("Deleted Customers Count: " + deletedCustomers.Count);

                // Xóa khách hàng khỏi database
                _dbcontext.Customers.Remove(cus);
                await _dbcontext.SaveChangesAsync();

                // Kiểm tra lại số lượng sau khi xóa
                deletedCustomersJson = HttpContext.Session.GetString("DeletedCustomers");
                Console.WriteLine("Updated Deleted Customers JSON: " + deletedCustomersJson);
            }

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Rollback()
        {
            var deletedCustomersJson = HttpContext.Session.GetString("DeletedCustomers");

            if (!string.IsNullOrEmpty(deletedCustomersJson))
            {
                var deletedCustomers = JsonConvert.DeserializeObject<List<Customer>>(deletedCustomersJson);

                foreach (var customer in deletedCustomers)
                {
                    if (customer != null)
                    {
                        customer.CustomerId = 0; 

                        _dbcontext.Customers.Add(customer);
                    }
                }
                await _dbcontext.SaveChangesAsync();

                HttpContext.Session.Remove("DeletedCustomers");
                var message = "";

                foreach (var customer in deletedCustomers)
                {
                    message += $"Customer với tên {customer.FullName}, SĐT {customer.PhoneNumber}, Email: {customer.Email}<br/>";
                }
                TempData["Message"] = message;
                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = "No customer data found to rollback.";
            return RedirectToAction(nameof(Index));
        }
    }
}
