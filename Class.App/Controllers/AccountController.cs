﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.App.Models;
using School.DAL.Entities;

namespace School.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var user = await _userManager.FindByEmailAsync(loginModel.EmailAdress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginModel);
            }
            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(loginModel);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAdress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new User()
            {
                Email = registerViewModel.EmailAdress,
                UserName = registerViewModel.EmailAdress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, registerViewModel.Role);
            }
            else
            {
                TempData["Error"] = newUserResponse.Errors.ToString();
                return View(registerViewModel);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
