using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace StufShop.Models
{
    public class User : IdentityUser
    {   
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        
    }
}
