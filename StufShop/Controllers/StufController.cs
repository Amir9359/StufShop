using Microsoft.AspNetCore.Mvc;
using StufShop.Models.Stuf;
using StufShop.Models.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.InfraStructure;

namespace StufShop.Controllers
{
    public class StufController : Controller
    {
        private IStufRepository _StufRepository;
        public StufController(IStufRepository StufRepository)
        {
            _StufRepository = StufRepository;
        }
        public async Task<IActionResult> Index(int id)
        {
            var Food = await _StufRepository.StufDetailAsync(id);
            return View(Food);
        }
 
        public async Task<IActionResult> FoodListByGroup(int Groupid)
        {
            var Foodview = new List<StufListView>();

            var Plist = await _StufRepository.SearchAsync(Groupid);
            foreach (var item in Plist)
            {
                Foodview.Add(new StufListView
                {
                    Id = item.Id,
                    PrimaryTitle = item.Title,
                    ImageUrl = $"{item.Id}.jpg",
                    Price = item.StufItems.Select(p => p.Price).FirstOrDefault().ToString("N0"),
                    Group = item.group
                });
            }
            return View("List", Foodview);
        }
        public IActionResult SendComment(string comment, int FoodId)
        {
            
            return new RedirectResult("/Food/Index/" + FoodId);
        }
    }
}