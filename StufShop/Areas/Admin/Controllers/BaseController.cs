using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StufShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models;

namespace StufShop.Areas.Admin.Controllers
{
    
    public class BaseController : Controller
{
        private UserManager<User> UserManager;
        private User _Operator;
        public BaseController(UserManager<User> UserManager )
        {
            this.UserManager = UserManager;
        }
        public User Operator {
            get
            {
                return GetOperator().GetAwaiter().GetResult();
            }
        }
        private async Task<User> GetOperator()
        {
            _Operator=await UserManager.FindByNameAsync("StufAdmin");
                if (await UserManager.CheckPasswordAsync(_Operator, "iArR6@R7t"))
                {
                    return  _Operator;
                    
                }   
                else
                {  
                  return null;
                }

        }

}
}
