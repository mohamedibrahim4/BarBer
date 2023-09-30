using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarBerEMR.Models
{
    public class InvoicePackages
    {
        public int Id { get; set; }
        //[Key, Column(Order = 0)]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int PackagesID { get; set; }

        public Packages Packages { get; set; }

    }
}
