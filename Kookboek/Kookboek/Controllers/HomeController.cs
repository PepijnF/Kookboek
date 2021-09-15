using System.Diagnostics;
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
        private readonly IRecipeLogic _recipeLogic;

        public HomeController(ILogger<HomeController> logger, IRecipeLogic recipeLogic)
        {
            _logger = logger;
            _recipeLogic = recipeLogic;
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
            Recipe recipe = await _recipeLogic.GetRecipeFromDb();
            RecipeModel recipeModel = new RecipeModel
            {
                ImageBase64 = System.Convert.ToBase64String(recipe.Image)
            };

            return View(recipeModel);
        }

        [HttpPost]
        public IActionResult PostRecipe(RecipeModel recipeModel)
        {
            if (recipeModel.Image.Length > 0)
            {
                _recipeLogic.SendRecipeToDb(_recipeLogic.RecipeModelToDto(recipeModel.Image, recipeModel.Title, recipeModel.Ingredients, recipeModel.Preparation));
            }
            else
            {
                
            }
            
            return RedirectToAction("ShowRecipe");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}