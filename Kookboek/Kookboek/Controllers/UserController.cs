using System;
using System.Collections.Generic;
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
        private RecipeContainer _recipeContainer;
        
        public UserController(UserContainer userContainer, RecipeContainer recipeContainer)
        {
            _userContainer = userContainer;
            _recipeContainer = recipeContainer;
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
                session.Set("Username", Encoding.ASCII.GetBytes(user.Username));
            }
            // TODO Some kind of fail statement
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
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
                // TODO Username already exists
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ShowRecipes()
        {
            var session = HttpContext.Session;
            byte[] userBytes;
            string userId;
            if (session.TryGetValue("user_id", out userBytes))
            {
                userId = Encoding.UTF8.GetString(userBytes, 0, userBytes.Length);
            }
            else
            {
                userId = "0";
            }

            List<Recipe> recipes = _recipeContainer.FindAllByUserId(userId);
            List<RecipeModel> recipeModels = new List<RecipeModel>();

            foreach (var recipe in recipes)
            {
               recipeModels.Add(new RecipeModel()
               {
                   Id = recipe.Id,
                   Title = recipe.Title,
                   Ingredients = recipe.Ingredients,
                   Preparation = recipe.Preparations,
                   ImageBase64 = recipe.FoodImage.Base64Image() 
               }); 
            }

            return View(recipeModels);
        }

        public IActionResult SignOut()
        {
            var session = HttpContext.Session;
            session.Remove("Username");
            session.Remove("user_id");
            return RedirectToAction("Index", "Home");
        }
    }
}