using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models;
using StufShop.Models.Carts;
using StufShop.Models.Stuf;

namespace StufShop.Controllers
{
    public class CartController : Controller
{
        private UserManager<User> _UserManager;
        private ICartRopository _CartRopo;
        private IStufRepository _StufRepo;
        public CartController(UserManager<User> UserManager, ICartRopository CartRopo, IStufRepository StufRepo)
        {
            _UserManager = UserManager;
            _CartRopo = CartRopo;
            _StufRepo = StufRepo;
        }
    public async Task<IActionResult> Index(int? stufid, int? count = 1 )
    {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Signin", "Account");
            }
            else 
            {
                var user = await _UserManager.FindByNameAsync(User.Identity.Name);
                var cart = await _CartRopo.Find(user.Id);
                if (stufid != null)
                {
                    var Stuf = await _StufRepo.FindAsync((int)stufid);
                    if (cart != null && Stuf.Count >= count)
                    {
                        if (cart.cartItems.Any(c=>c.Stuf.Id == stufid ))
                        {
                            var cartItem = cart.cartItems.FirstOrDefault(c => c.Stuf.Id == stufid);
                            await _CartRopo.Update(new CartItems
                            {
                                Id=cartItem.Id,
                                number= (int)count ,
                                Price = Stuf.Price,
                            });
                            await _CartRopo.save();
                            return RedirectToAction("Index", "Order");
                        }
                        else
                        {
                            await _CartRopo.Add(new CartItems
                            {
                                Cart = cart,
                                Stuf = Stuf,
                                number = (int)count,
                                Price = Stuf.Price
                            });

                            await _CartRopo.save();

                            return RedirectToAction("Index" , "Order");
                        }

                    }
                    else
                    {
                        var newcart = new Cart
                        {
                            Customer = user,
                            Date = DateTime.Now,
                            PayState = PayState.UnPaied,
                            OrderDate = DateTime.Now
                        };
                        await _CartRopo.Add(newcart);
                        await _CartRopo.save();

                        var cartfinal = await _CartRopo.Find(newcart.Id);
                        await _CartRopo.Add(new CartItems
                        {
                            Cart = cartfinal,
                            Stuf = Stuf,
                            number = (int)count,
                            Price = Stuf.Price
                        });

                        await _CartRopo.save();

                        return RedirectToAction("Index", "Order");
                    }
                    
                }
                else 
                {
      
                    return RedirectToAction("List","Home");
                }
            }
        
    }
    public async Task<IActionResult> Remove(int id)
    {
           await _CartRopo.Delete(id);
            await _CartRopo.save();
            return new RedirectResult("/Cart");
    }
}
}
