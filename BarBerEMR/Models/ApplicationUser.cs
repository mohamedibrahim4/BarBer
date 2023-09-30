using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBerEMR.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string PassWord { get; set; }
    }
}
