using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StufShop.Models;
using StufShop.Models.Stuf;
using Microsoft.EntityFrameworkCore;

namespace StufShop.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<User> UserManager;
        private SignInManager<User> SignInManager;
        private IStufRepository _StufRepository;
        private ApplicationDbContext _context;
        public HomeController(UserManager<User> UserManager, SignInManager<User> SignInManager , IStufRepository StufRepository,
            ApplicationDbContext context)
        {
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
            _StufRepository = StufRepository;
            _context = context;
        }
        public async  Task<IActionResult> Index()
        {
            if (User.Identity.Name != null )
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var claim = await UserManager.GetClaimsAsync(user);
                if (user.UserName == "admin")
                {
                    await SignInManager.SignOutAsync();
                    return Redirect("/");
                }
            }

            return View();
        }
        public async Task<IActionResult> List( )
        {
            var stuf = await _context.Stufs.ToListAsync();

            return View("index-2", stuf);

        }

        public async Task<IActionResult> ViewAllStuf()
        {
            var stufs =await _context.Stufs.ToListAsync();
            
            return View(stufs);
        }
        public async Task<IActionResult> Details(int id)
        {
            var stuf =await _StufRepository.FindAsync(id);
            
            return View(stuf);
        }

    }
}
