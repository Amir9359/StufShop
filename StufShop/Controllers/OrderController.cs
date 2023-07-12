using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StufShop.Models;
using StufShop.Models.Stuf;
using StufShop.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.InfraStructure;
using StufShop.Models.Carts;



namespace StufShop.Controllers
{
    public class OrderController : Controller
    {
        private ICartRopository _CartRopo;
        private UserManager<User> _UserManager;
        private IAddressRepository _AddressRepo;
        private ApplicationDbContext _context;
        private ICartItemRepository _CartItemRepo;
        private IStufItemRepository _StufItemRepo;
        public OrderController(IStufItemRepository StufItemRepo, ICartRopository CartRopo,
            ICartItemRepository CartItemRepo, UserManager<User> UserManager, IAddressRepository AddressRepo, ApplicationDbContext context)
        {
            _UserManager = UserManager;
            _CartRopo = CartRopo;
            _AddressRepo = AddressRepo;
            _context = context;
            _CartItemRepo = CartItemRepo;
            _StufItemRepo = StufItemRepo;
        }
        public async Task<IActionResult> Index()
        {
            //-------cart-----------
            var customer = await _UserManager.FindByNameAsync(User.Identity.Name);
            var cart = await _CartRopo.Find(customer.Id);
            if (cart == null) 
            {
                ViewBag.TotalPrice = "0";
            }
            else
            {
                ViewBag.TotalPrice = cart.cartItems.Sum(p => p.Stuf.Price * p.number).ToString("N0");
            }

            //------------------Address--------------
            var Address = await _AddressRepo.Find(customer.Id);
            if (Address != null)
            {
                return View(Address);
            }
            else
            {
                return RedirectToAction("Index", "Profile");
            }
        }
        public async Task<IActionResult> Save(string Paynumber , string Date)
        {
            //------------cart-----------
            var customer = await _UserManager.FindByNameAsync(User.Identity.Name);
            var cart = await _CartRopo.Find(customer.Id);
            var TotalPrice = cart.cartItems.Sum(p => p.Stuf.Price * p.number);
            //-----------------------addOrder-----------------------
            var Payed = new CartPayed();
            Payed = new CartPayed
            {
                Date = Date,
                Paynumber = Paynumber,
                Cusstomer = customer
            };
            await _context.CartPayeds.AddAsync(Payed);
            await _context.SaveChangesAsync();

            Payed.CartPayedItemID = Payed.Id;

            var payedItem = new List<CartItemPayed>();
            foreach (var payItem in cart.cartItems)
            {
                payedItem.Add(new CartItemPayed
                    {
                        ProductId = payItem.Stuf.Id,
                        CartPayedId = Payed.Id,
                        Quantity = payItem.number
                    }
                );
            }
            await _context.CartItemPayeds.AddRangeAsync(payedItem);
            await _context.SaveChangesAsync();

            var order = new Cart
            {
                Customer = customer,
                OrderDate = DateTime.Now,
                TotlaPrice = TotalPrice,
                PayState = PayState.Paied,
            };
 

            var OrderItems = new List<CartItems>();
            foreach (var cartItems in cart.cartItems)
            {
                OrderItems.Add(new Models.Carts.CartItems
                {
                    Cart = order,
                    Price = cartItems.Stuf.Price,
                    Stuf = cartItems.Stuf,
                    number = cartItems.number

                });

            }
            //-----------DeletCartInformation-------------
            _CartRopo.DeletCartItems(cart.cartItems);
            await _CartRopo.save();

            await _CartRopo.DeleteCart(cart.Id);
            await _CartRopo.save();


            return RedirectToAction("Index" , "Home");
        }
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            ////اپدیت موجودی کالا/////
            var cartItem = await _CartItemRepo.FindItem(id);
            var Product = await _context.Stufs.FindAsync(cartItem.Stuf.Id);
            Product.Count = Product.Count + cartItem.number;
            await _context.SaveChangesAsync();


            var res = await _CartItemRepo.DeleteCartItem(id);
            if (res)
            {

                await _CartItemRepo.Save();
                return RedirectToAction("Index", "Order");
            }
            return RedirectToAction("Index", "Order");
        }


    }
}
