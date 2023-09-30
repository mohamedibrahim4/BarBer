using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarBerEMR.ViewModels
{
    public class RoleFormViewModel
    {
        [Required]
        [MaxLength(200)]
        public string Name  { get; set; }
    }
}
