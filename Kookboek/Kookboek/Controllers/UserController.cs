using System;
using System.Text;
using AbstractionLayer;
using Kookboek.Models;
using LogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace Kookboek.Controllers
{
    public class UserController : Controller
    {
        private UserContainer _userContainer;
        
        public UserController()
        {
            _userContainer = new UserContainer();
        }

        // GET
        public IActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult LogIn(UserModel userModel)
        {
            

            User user = _userContainer.FindByUsername(userModel.Username);
            if (user.Password == userModel.Password)
            {
                var session = HttpContext.Session;
                session.Set("user_id", Encoding.ASCII.GetBytes(user.Id));
            }
            // Some kind of fail statement
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Register(UserModel userModel)
        {
            if (!_userContainer.GetAll().Exists(u => u.Username == userModel.Username))
            {
                _userContainer.AddUser(new User()
                {
                    Username = userModel.Username,
                    Password = userModel.Password
                });
            }
            else
            {
                // Username already exists
            }

            return RedirectToAction("Index", "Home");
        }
    }
}