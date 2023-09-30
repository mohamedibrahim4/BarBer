using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarBerEMR.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public double AmountWithVat { get; set; }
        //public double PaidAmount { get; set; }
        //public double RemainingAmount { get; set; }
        //public double discount { get; set; }

        public double Vat { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        //Relation
        [ForeignKey("PaymentMethod")]
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<InvoicePackages> PackagesInvoice { get; set; } = new List<InvoicePackages>();



        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
