using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarBerEMR.ViewModels
{
    public class PackagesViewModel
    {
        public int ID { get; set; }
        [Required, MaxLength(100)]
        public string PackageName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public double PackagePrice { get; set; }
    }
}
