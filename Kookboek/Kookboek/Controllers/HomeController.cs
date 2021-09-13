using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [HttpPost]
        public IActionResult PostRecipe(RecipeModel recipeModel)
        {
            _saveRecipe.SendRecipeToDb(recipeModel.ConvertToRecipe());
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}