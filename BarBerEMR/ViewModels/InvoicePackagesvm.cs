﻿using BarBerEMR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarBerEMR.ViewModels
{
    public class InvoicePackagesvm

    {
        public int Id { get; set; }
        //public double TotalAmount { get; set; }
        //public double PaidAmount { get; set; }
        //public double RemainingAmount { get; set; }
        //public double discount { get; set; }
        public double Amount { get; set; }
        public double AmountWithVat { get; set; }
        public double Vat { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        //public virtual ICollection<Packages> Packages { get; set; }
        public string UserID { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    
        public int?   InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int? PackageId { get; set; }
        public Packages Packages { get; set; }
        public int[] SkillID { get; set; } 
    }
}
