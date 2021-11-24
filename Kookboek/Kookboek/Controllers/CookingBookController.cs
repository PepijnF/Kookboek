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
        private readonly CookingBookContainer _cookingBookContainer;
        
        public CookingBookController(RecipeContainer recipeContainer, ICookingBookDal cookingBookDal, CookingBookContainer cookingBookContainer)
        {
            _recipeContainer = recipeContainer;
            _cookingBookDal = cookingBookDal;
            _cookingBookContainer = cookingBookContainer;
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
                OwnerId = userId,
                Name = newCookingBookModel.CookingBookModel.Name,
                Description = newCookingBookModel.CookingBookModel.Description,
                Recipes = recipes
            };
            cookingBook.Save();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ShowCookingBooks()
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

            List<CookingBook> cookingBooks = _cookingBookContainer.GetAllByUserId(userId);

            List<CookingBookModel> cookingBookModels = new List<CookingBookModel>();
            foreach (var cookingBook in cookingBooks)
            {
                List<RecipeModel> models = new List<RecipeModel>();
                foreach (var recipe in _recipeContainer.FindAllByCookingBookId(cookingBook.Id))
                {
                    models.Add(new RecipeModel()
                    {
                       Id = recipe.Id,
                       Title = recipe.Title,
                       Ingredients = recipe.Ingredients,
                       Preparation = recipe.Preparations,
                       ImageBase64 = recipe.FoodImage.Base64Image()
                    });
                }

                cookingBookModels.Add(new CookingBookModel()
               {
                   Id = cookingBook.Id,
                   Name = cookingBook.Name,
                   Description = cookingBook.Description,
                   RecipeModels = models
               }); 
            }

            return View(cookingBookModels);
        }

        public IActionResult ShowCookingBook(string cookingBookId)
        {
            var cookingBook = _cookingBookContainer.FindById(cookingBookId);
            List<RecipeModel> recipeModels = new List<RecipeModel>();
            foreach (var recipe in cookingBook.Recipes)
            {
               recipeModels.Add(new RecipeModel()
               {
                   Id = recipe.Id,
                   ImageBase64 = recipe.FoodImage.Base64Image(),
                   Ingredients = recipe.Ingredients,
                   Title = recipe.Title,
                   Preparation = recipe.Preparations
               }); 
            }
            
            return View(new CookingBookModel()
            {
                Id = cookingBook.Id,
                Name = cookingBook.Name,
                Description = cookingBook.Description,
                RecipeModels = recipeModels
            });
        }
    }
}