using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StufShop.Models;
using StufShop.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StufShop.Controllers
{
    public class ProfileController : Controller
    {

        private IAddressRepository _AddressRepository;
        private IUserValidator<User> _UserValidator;
        private SignInManager<User> _signInManager;
        private UserManager<User> _UserManager;
        private IPasswordHasher<User> _passwordHasher;
        private IPasswordValidator<User> _passwordValidator;
        public ProfileController(IAddressRepository AddressRepository, UserManager<User> UserManager, IUserValidator<User> UserValidator,
            IPasswordValidator<User> passwordValidator, IPasswordHasher<User> passwordHasher, SignInManager<User> signInManager)
        {
            _UserManager = UserManager;
            _AddressRepository = AddressRepository;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _UserValidator = UserValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(string province, string city, string address, string tel)
        {
            var customer = await _UserManager.FindByNameAsync(User.Identity.Name);
            await _AddressRepository.Add(new Address
            {
                city = city,
                Country = province,
                Location = address,
                Phone = tel,
                Customer = customer
            });
            await _AddressRepository.Save();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Details()
        {
            var user = await _UserManager.FindByNameAsync(User.Identity.Name);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Details(string Fname, string Lname, string Username, string Password, string ConformPassword)
        {
            var user = await _UserManager.FindByNameAsync(User.Identity.Name);
            if (Password != ConformPassword && !string.IsNullOrEmpty(Password))
            {
                ViewBag.Message = "رمز عبور با تاییده رمز عبور برابر نیست !";
                return View(user);
            }
  
            user.FirstName = Fname;
            user.LastName = Lname;

            var validEmail = await _UserValidator.ValidateAsync(_UserManager, user);
            var passiden = await _passwordValidator.ValidateAsync(_UserManager, user, Password);
            if (passiden.Succeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, Password); 
            }
            else
            {
                AddErrorsFromResult(passiden);
            }

            await _signInManager.PasswordSignInAsync(user, Password, true, false);
            var result = await _UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                AddErrorsFromResult(result);
            }
            return View();
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
