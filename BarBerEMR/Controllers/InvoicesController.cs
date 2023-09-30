using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarBerEMR.Data;
using BarBerEMR.Models;
using BarBerEMR.ViewModels;
using Microsoft.AspNetCore.Http;

namespace BarBerEMR.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            return View(await _context.Invoice.Include(x=>x.PackagesInvoice).ThenInclude(f=>f.Packages).Include(c=>c.PaymentMethod).Include(a=>a.ApplicationUser).ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewBag.UserID = new SelectList(_context.Users, "Id", "UserName");
            var Packages = _context.Packages.ToList();
            ViewBag.Packages = Packages;
            ViewBag.PaymentMethod = new SelectList(_context.PaymentMethod.ToList(), "ID", "PaymentMethodName");

            
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,TotalAmount,PaidAmount,RemainingAmount,discount,Vat,InvoiceDate")] Invoice invoice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(invoice);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(invoice);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection formval,InvoicePackagesvm vm)
        {
            string result = Request.Form["SkillID"];
            string[] PackagesLst = result.Split(",");
            PackagesLst = PackagesLst.SkipLast(1).ToArray();
            var invoice = new Invoice()
            {
                Amount = vm.Amount,
                AmountWithVat = vm.AmountWithVat,
                //RemainingAmount = vm.RemainingAmount,
                //discount = vm.discount,
                Vat = vm.Vat,
                InvoiceDate = vm.InvoiceDate,
                PaymentMethod=vm.PaymentMethod,
                ApplicationUserId=vm.UserID,
                PaymentMethodId=vm.PaymentMethodId

            };

            foreach (var item in PackagesLst)
            {
                invoice.PackagesInvoice.Add(new InvoicePackages()
                {
                    InvoiceId = invoice.Id,
                    PackagesID = int.Parse(item)
                });
            }

            await _context.Invoice.AddAsync(invoice);
            await  _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.UserID = new SelectList(_context.Users, "Id", "UserName");
            var Packages = _context.Packages.ToList();
            ViewBag.Packages = Packages;
            ViewBag.PaymentMethod = new SelectList(_context.PaymentMethod.ToList(), "ID", "PaymentMethodName");

            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.FindAsync(id);
            InvoicePackagesvm invoicePackagesvm = new InvoicePackagesvm();
            invoicePackagesvm.Id = invoice.Id;
            invoicePackagesvm.Amount = invoice.Amount;
            invoicePackagesvm.AmountWithVat = invoice.AmountWithVat;
            invoicePackagesvm.Vat = invoice.AmountWithVat;
            invoicePackagesvm.InvoiceDate = invoice.InvoiceDate;
            invoicePackagesvm.PaymentMethodId = invoice.PaymentMethodId;
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoicePackagesvm);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,IFormCollection formval, InvoicePackagesvm vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            string result = Request.Form["SkillID"];
            string[] PackagesLst = result.Split(",");
            PackagesLst = PackagesLst.SkipLast(1).ToArray();
            var invoice = new Invoice()
            {
                Amount = vm.Amount,
                AmountWithVat = vm.AmountWithVat,
                //RemainingAmount = vm.RemainingAmount,
                //discount = vm.discount,
                Vat = vm.Vat,
                InvoiceDate = vm.InvoiceDate,
                PaymentMethod = vm.PaymentMethod,
                ApplicationUserId = vm.UserID,
                PaymentMethodId = vm.PaymentMethodId

            };

            foreach (var item in PackagesLst)
            {
                invoice.PackagesInvoice.Add(new InvoicePackages()
                {
                    InvoiceId = invoice.Id,
                    PackagesID = int.Parse(item)
                });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
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
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoice.FindAsync(id);
            _context.Invoice.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoice.Any(e => e.Id == id);
        }
    }
}
