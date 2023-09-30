using BarBerEMR.Data;
using BarBerEMR.Models;
using BarBerEMR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBerEMR.Controllers.api
{
   // [Route("api/[Controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PackagesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        //public PackagesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        ////[Route("api/Packages/Get")]
        //[HttpGet]
        //public  List<PackagesViewModel> GetAllPackages()
        //{
        //    var Packages =  _context.Packages.ToList();
        //    List<PackagesViewModel> lst = new List<PackagesViewModel>();
        //    //var cli = (from c in db.SliderImages select c).ToList();
        //    foreach (var item in Packages)
        //    {
        //        PackagesViewModel obj = new PackagesViewModel();
        //        obj.ID = item.ID;
        //        obj.PackageName = item.PackageName;
        //        obj.PackagePrice = item.PackagePrice;
        //        lst.Add(obj);
        //    }
        //    return lst;
        //}
      
    }
}
