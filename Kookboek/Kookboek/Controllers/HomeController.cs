using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AbstractionLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kookboek.Models;

namespace Kookboek.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISaveRecipe _saveRecipe;

        public HomeController(ILogger<HomeController> logger, ISaveRecipe saveRecipe)
        {
            _logger = logger;
            _saveRecipe = saveRecipe;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ShowRecipe()
        {
            Recipe recipe = await _saveRecipe.GetRecipeFromDb();
            RecipeModel recipeModel = new RecipeModel();
            recipeModel.ImageBase64 = System.Convert.ToBase64String(recipe.Image);

            return View(recipeModel);
        }

        [HttpPost]
        public IActionResult PostRecipe(RecipeModel recipeModel)
        {
            // IFormFile to Byte[]
            long length = recipeModel.Image.Length;

            if (length < 0)
            {
                // Bad image
            }

            using Stream fileStream = recipeModel.Image.OpenReadStream();
            byte[] byteImage = new byte[length];
            fileStream.Read(byteImage, 0, (int) recipeModel.Image.Length);
            
            _saveRecipe.SendRecipeToDb(new Recipe()
            {
                Image = byteImage,
                Ingredients = recipeModel.Ingredients,
                Preparation = recipeModel.Preparation,
                Title = recipeModel.Title
            });
            
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}