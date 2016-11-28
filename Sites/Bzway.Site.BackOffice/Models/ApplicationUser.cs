using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bzway.Site.BackOffice.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string MobilePhoneNumber { get; set; }
    }
    public class MyTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
