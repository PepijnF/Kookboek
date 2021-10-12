using System;
using System.IO;
using System.Text;
using Kookboek.Models;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kookboek.Controllers
{
    public class RecipeController : Controller
    {
        private UserContainer _userContainer;
        private RecipeContainer _recipeContainer;

        public RecipeController(UserContainer userContainer, RecipeContainer recipeContainer)
        {
            _userContainer = userContainer;
            _recipeContainer = recipeContainer;
        }
        
        private byte[] FormFileToByteArray(IFormFile file)
        {
            long fileLength = file.Length;
            using Stream fileStream = file.OpenReadStream();
            byte[] byteFile = new byte[fileLength];
            fileStream.Read(byteFile, 0, (int) file.Length);
        
            return byteFile;
        }
        
        // GET
        public IActionResult AddRecipe()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRecipe(RecipeModel recipeModel)
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

            string recipeId = Guid.NewGuid().ToString();

            Recipe recipe = new Recipe()
            {
                Id = recipeId,
                Ingredients = recipeModel.Ingredients,
                Preparations = recipeModel.Preparation,
                Title = recipeModel.Title,
                UserId = userId,
                FoodImage = new FoodImage()
                {
                    Id = Guid.NewGuid().ToString(),
                    Image = FormFileToByteArray(recipeModel.Image),
                    RecipeId = recipeId
                }
            };

            User user = _userContainer.FindById(userId);
            user.AddRecipe(recipe);

            _recipeContainer.AddRecipe(recipe);
            
            return RedirectToAction("Index", "Home");
        }
    }
}