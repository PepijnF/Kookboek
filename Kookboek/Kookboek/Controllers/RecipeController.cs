using System;
using System.IO;
using System.Text;
using AbstractionLayer;
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
        private IFoodImageDal _foodImageDal;

        public RecipeController(UserContainer userContainer, RecipeContainer recipeContainer, IFoodImageDal foodImageDal)
        {
            _userContainer = userContainer;
            _recipeContainer = recipeContainer;
            _foodImageDal = foodImageDal;
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

        public IActionResult ShowRecipe(string id)
        {
            Recipe recipe = _recipeContainer.FindById(id);
            Console.WriteLine(recipe.Title);

            RecipeModel recipeModel = new RecipeModel()
            {
                Id = recipe.Id,
                ImageBase64 = recipe.FoodImage.Base64Image(),
                Ingredients = recipe.Ingredients,
                Preparation = recipe.Preparations,
                Title = recipe.Title
            };
            // TODO change view
            return View("ShowRecipe", recipeModel);
        }

        public IActionResult EditRecipe(string recipeId)
        {
            var recipe = _recipeContainer.FindById(recipeId);
            var recipeModel = new RecipeModel()
            {
                Id = recipe.Id,
                ImageBase64 = recipe.FoodImage.Base64Image(),
                Ingredients = recipe.Ingredients,
                Title = recipe.Title,
                Preparation = recipe.Preparations
            };
            return View(new EditRecipeModel(){OldRecipeModel = recipeModel});
        }

        [HttpPost]
        public IActionResult EditRecipe(EditRecipeModel editRecipeModel)
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
            
            Recipe recipeDb = _recipeContainer.FindById(editRecipeModel.EditedRecipeModel.Id);
            FoodImage foodImage;
            if (editRecipeModel.EditedRecipeModel.Image != null)
            {
                _foodImageDal.RemoveByRecipeId(editRecipeModel.EditedRecipeModel.Id);
                foodImage = new FoodImage()
                {
                    Id = Guid.NewGuid().ToString(),
                    Image = FormFileToByteArray(editRecipeModel.EditedRecipeModel.Image),
                    RecipeId = editRecipeModel.EditedRecipeModel.Id
                };
            }
            else
            {
                foodImage = recipeDb.FoodImage;
            }

            Recipe recipe = new Recipe()
            {
                Id = editRecipeModel.EditedRecipeModel.Id,
                Ingredients = editRecipeModel.EditedRecipeModel.Ingredients,
                Preparations = editRecipeModel.EditedRecipeModel.Preparation,
                Title = editRecipeModel.EditedRecipeModel.Title,
                FoodImage = foodImage,
                UserId = userId 
            };
            
            _recipeContainer.EditRecipe(recipe);
            return RedirectToAction("Index", "Home");
        }
    }
}