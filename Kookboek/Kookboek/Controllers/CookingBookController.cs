using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbstractionLayer;
using Kookboek.Models;
using LogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kookboek.Controllers
{
    public class CookingBookController: Controller
    {
        private readonly RecipeContainer _recipeContainer;
        private readonly ICookingBookDal _cookingBookDal;
        
        public CookingBookController(RecipeContainer recipeContainer, ICookingBookDal cookingBookDal)
        {
            _recipeContainer = recipeContainer;
            _cookingBookDal = cookingBookDal;
        }
        
        [HttpGet]
        public IActionResult CreateCookingBook()
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
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var recipe in recipes)
            {
               items.Add(new SelectListItem()
               {
                   Text = recipe.Title,
                   Value = recipe.Id
               }); 
            }

            var newCookingBookModel = new NewCookingBookModel()
            {
                RecipeModels = items
            };

            return View(newCookingBookModel);
        }

        [HttpPost]
        public IActionResult CreateCookingBook(NewCookingBookModel newCookingBookModel, string[] selectedRecipes)
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

            List<Recipe> recipes = new List<Recipe>();
            foreach (var recipeId in selectedRecipes)
            {
                recipes.Add(_recipeContainer.FindById(recipeId));
            }

            var cookingBook = new CookingBook(_cookingBookDal)
            {
                Name = newCookingBookModel.CookingBookModel.Name,
                Description = newCookingBookModel.CookingBookModel.Description,
                Recipes = recipes
            };
            cookingBook.Save();
            return RedirectToAction("Index", "Home");
        }
    }
}