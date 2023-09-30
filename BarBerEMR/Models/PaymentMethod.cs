using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarBerEMR.Models
{
    public class PaymentMethod
    {
        public int ID { get; set; }
        [Required,MaxLength(100)]
        public string PaymentMethodName { get; set; }
    }
}
