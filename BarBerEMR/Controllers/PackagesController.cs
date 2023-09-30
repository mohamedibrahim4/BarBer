using BarBerEMR.Data;
using BarBerEMR.Models;
using BarBerEMR.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBerEMR.Controllers
{
    public class PackagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PackagesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> CreateNewPackage(string PackageName,string PackagePrice)

        {

            if (PackagePrice == null && PackageName == null )
                return Json(new { messege = "false" });



            if (PackagePrice != null && PackageName != null)
            {
                var PackageObj = new Packages();
                PackageObj.PackageName = PackageName;
                PackageObj.PackagePrice =double.Parse( PackagePrice);

             _context.Packages.Add(PackageObj);
               await _context.SaveChangesAsync();

                // return Json("Branch Update SUCCessfully");
                return Json(new { messege = "true" });

            }
            else
            {
                return Json(new { messege = "false" });

            }


        }

        [HttpDelete]
        public async Task<JsonResult> DeletePackage(int id)
        {


            Packages packages = _context.Packages.Find(id);
            if (packages == null)
                return Json(new { messege = "false" });
            _context.Packages.Remove(packages);


            //return Json(new { messege = "false" });
            var Success=false;
            if (Success = await _context.SaveChangesAsync() > 0)
            {
                return Json(new { messege = "true" });

            }
            else
            {
                return Json(new { messege = "false" });

            }
            //await _context.SaveChangesAsync();


        }
        [HttpGet]
        public List<PackagesViewModel> GetAllPackages()
        {
            var Packages = _context.Packages.ToList();
            List<PackagesViewModel> lst = new List<PackagesViewModel>();
            //var cli = (from c in db.SliderImages select c).ToList();
            foreach (var item in Packages)
            {
                PackagesViewModel obj = new PackagesViewModel();
                obj.ID = item.ID;
                obj.PackageName = item.PackageName;
                obj.PackagePrice = item.PackagePrice;
                lst.Add(obj);
            }
            return lst;


        }
        [HttpPost]
        public async Task<JsonResult> UpdatePackageNameById(int id,string PackageName, string PackagePrice)

        {

            if (PackagePrice == null && PackageName == null || id==0)
                return Json(new { messege = "false" });


            if (PackagePrice != null && PackageName != null)
            {
                var PackageObj = _context.Packages.Find(id);
                PackageObj.PackageName = PackageName;
                PackageObj.PackagePrice = double.Parse(PackagePrice);


                _context.Packages.Find(id).PackageName = PackageName;
                _context.Packages.Find(id).PackagePrice = double.Parse( PackagePrice);

                //_context.Packages.Add(PackageObj);
                await _context.SaveChangesAsync();
                return Json(new { messege = "true" });


            }
            else
            {
                return Json(new { messege = "false" });

            }
        }

        #region invoice
        public IActionResult IndexInvoice()
        {
            return View();
        }
        public IActionResult _IndexInvoice()
        {
            //var tasks = db.Tasks.Where(t => t.applicationUserID == id).Include(t => t.applicationUser).Include(t => t.facility).Include(t => t.software);
            //var Invoice = _context.Invoice.ToList().Include(t => t.ApplicationUser).Include(t => t.PaymentMethod).Include(t => t.software);

            //_context.Invoice.Select().Include(t => t.applicationUser).Include(t => t.facility).Include(t => t.software);
            return View();
        }

        #endregion
    }
}
