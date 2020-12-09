using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop1.Data;

namespace Shop1.Controllers
{
    public class CustomersController : Controller
    {
        private readonly Shop1Context _context;

        public CustomersController(Shop1Context context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string sortOrder, string customerName)        {

            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "firstName_desc" : "";
            ViewData["LastNameSortParm"] = sortOrder == "lastName" ? "lastName_desc" : "lastName";
            ViewData["CitySortParm"] = sortOrder == "city" ? "city_desc" : "city";

            ViewData["CurrentFilter"] = customerName;

            var customer = from c in _context.Customer
                         select c;

            if (!String.IsNullOrEmpty(customerName))
            {
                customer = customer.Where(c => c.FirstName.Contains(customerName) 
                || c.LastName.Contains(customerName));
            }

            switch (sortOrder)
            {
                case "firstName_desc":
                    customer = customer.OrderByDescending(c => c.FirstName);
                    break;
                case "lastName":
                    customer = customer.OrderBy(c => c.LastName);
                    break;
                case "lastName_desc":
                    customer = customer.OrderByDescending(c => c.LastName);
                    break;
                case "city":
                    customer = customer.OrderBy(c => c.City);
                    break;
                case "city_desc":
                    customer = customer.OrderByDescending(c => c.City);
                    break;
                default:
                    customer = customer.OrderBy(c => c.FirstName);
                    break;
            }

            return View(await customer.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var customer = await _context.Customer
            //    .FirstOrDefaultAsync(m => m.CustomerID == id);

            var customer = await _context.Customer
                .Include(c => c.Orders)//tablica preko koje je vezan na tablicu product
                    .ThenInclude(e => e.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,FirstName,LastName,City")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,FirstName,LastName,City")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerID == id);
        }
    }
}
