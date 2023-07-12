using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StufShop.InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Digikala.Areas.Admin;
using StufShop.Models;
using StufShop.Models.Stuf;
using StufShop.Models.Groups;
using StufShop.Models.ViewModels.Product;
using StufShop.Models.Carts;
using StufShop.Models.Profile;
using StufShop.Models;


namespace StufShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StufController : BaseController
    {

        private UserManager<User> UserManager;
        private IStufRepository _StufRepo;
        private IGroupRepository groupRepo;
        private IHostingEnvironment hosting;
        private IStufItemRepository StufItemRepo;
        private ApplicationDbContext _context;

        public StufController(UserManager<User> userManager, IStufRepository StufRepo,
            IGroupRepository groupRepo, IHostingEnvironment hosting, IStufItemRepository StufItemRepo, ApplicationDbContext context) : base(userManager)
        {
            UserManager = userManager;
            _StufRepo = StufRepo;
            this.groupRepo = groupRepo;
            this.hosting = hosting;
            _context = context;
            StufItemRepo = StufItemRepo;
        }


        #region HttpGet 
        public IActionResult Index(int Id)
        {
            return View();
        }


        public async Task<IActionResult> list(int? id, string PrimaryTitle)
        {
            var breadcrumbs = new List<breadcrumb>()
            {
                new breadcrumb
                {
                    Title="صفحه اصلی ",
                    Url ="/"
                } ,
                new breadcrumb
                {
                    Title="لیست کالا ها ",
                    Url =""
                }
            };

            var products = await _StufRepo.SearchAsync(id, PrimaryTitle);
            var StufList = new List<StufShopView>();
            var persian = new PersianCalendar();
            if (products != null)
            {
                foreach (var item in products)
                {
                    StufList.Add(new StufShopView
                    {
                        Id = item.Id,
                        PrimaryTitle = item.Title,
                        Description = item.Description,
                        group = new Group
                        {
                            Id = item.group.Id,
                            GroupTitle = item.group.GroupTitle
                        },
                        state = item.state,
                        Creator = item.Creator?.FirstName + " " + item.Creator?.LastName,
                        CreateDate = persian.PersianDate(item.CreateDate),
                        Count = item.Count
                    });
                }

            }
            return View(StufList);
        }


        public async Task<IActionResult> Add()
        {

            return View();
        }


        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.Idd = 1;

            var GroupbrandModel = new Models.Stuf.GoupBrandView();

            var stuf = await _StufRepo.FindAsync(Id);
            GroupbrandModel.StufShop = new Stuf
            {
                Id = Id,
                groupid = stuf.groupid,
                Title = stuf.Title,
                state = stuf.state,
                Description = stuf.Description,
                Price = stuf.Price,
                Count = stuf.Count
            };
            var selectgroup = await groupRepo.FindGroupsAsync();

            GroupbrandModel.Groups = new SelectList(selectgroup, "Id", "Title");

            return View("Add", GroupbrandModel);
        }


        public async Task<IActionResult> Delete(int Id)
        {
            var stufCarts = await _context.cartItems.Include(d => d.Stuf)
                    .Where(s => s.Stuf.Id == Id).ToListAsync();
            _context.cartItems.RemoveRange(stufCarts);

            var Pitem = await _StufRepo.FindAsync(Id);
            if (Pitem != null)
            {
                await _StufRepo.DeleteAsync(Pitem.Id);
                await _StufRepo.SaveAsync();
            }

            return RedirectToAction("List");
        }

        #endregion /HttpGet

        #region HttpPost
        [HttpPost]
        public async Task<IActionResult> save(int? Id, string PrimaryTitle, string Description,
             int Group, int Price, int count, byte state, IFormFile Photo)
        {
            if (Id == null)
            {
                if (Photo.Length < 1000000)
                {


                    var user = _context.User.FirstOrDefault(s => s.UserName == User.Identity.Name);
                    Stuf Stuf = new Stuf()
                    {
                        groupid = Group,
                        Title = PrimaryTitle,
                        state = (State)state,
                        Description = Description,
                        Creator = user,
                        CreateDate = DateTime.Now,
                        Price = Price,
                        Count = count
                    };

                    await _StufRepo.AddAsync(Stuf);
                    await _StufRepo.SaveAsync();
                    var productid = Stuf.Id;

                    var ext = Path.GetExtension(Photo.FileName);
                    var path = Path.Combine(hosting.WebRootPath + "\\images\\itemPics\\page3_pic-" + productid + " " + ext);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await Photo.CopyToAsync(filestream);
                    }
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("Photo", "حداکثر سایز عکس باید  1 مگا بایت باشد !");
                    return View("Add");
                }
            }
            else
            {//edit
                var op = await UserManager.FindByIdAsync(this.Operator.Id);
                Stuf product = new Stuf
                {
                    Id = (int)Id,
                    groupid = Group,
                    Title = PrimaryTitle,
                    state = (State)state,
                    Description = Description,
                    Price = Price,
                    Count = count
                };
                _StufRepo.Update(product);
                await _StufRepo.SaveAsync();

                return RedirectToAction("List");
            }
        }



        #endregion /HttpPost
    }
}
