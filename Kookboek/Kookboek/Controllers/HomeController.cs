using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AbstractionLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kookboek.Models;
using LogicLayer;

namespace Kookboek.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeLogic _recipeLogic;

        private readonly IFoodImageDal _foodImageDal;
        private readonly IUserDal _userDal;
        private readonly IRecipeDal _recipeDal;

        public HomeController(ILogger<HomeController> logger, IRecipeLogic recipeLogic, IFoodImageDal foodImageDal, IUserDal userDal, IRecipeDal recipeDal)
        {
            _logger = logger;
            _recipeLogic = recipeLogic;

            _foodImageDal = foodImageDal;
            _userDal = userDal;
            _recipeDal = recipeDal;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.IsAvailable)
            {
                var session = HttpContext.Session;
                byte[] user;
                if (session.TryGetValue("user_id", out user))
                {
                    Console.WriteLine(Encoding.UTF8.GetString(user, 0, user.Length));
                    Console.WriteLine(session.Id);
                }
                else
                {
                    Console.WriteLine(session.Id);
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ShowRecipe()
        {
            RecipeDto recipeDto = (await _recipeLogic.GetRecipeFromDb())[0];
            RecipeModel recipeModel = new RecipeModel
            {
            };

            return View(recipeModel);
        }
        

        [HttpPost]
        public IActionResult PostRecipe([Bind(include:"Image")]RecipeModel recipeModel)
        {
            long length;
            try
            {
                length = recipeModel.Image.Length;
            }
            catch (Exception e)
            {
                length = 0;
                ModelState.AddModelError("Image", "File upload failed");
            }

            if (length > 0)
            {
                byte[] imageArray = _recipeLogic.FormFileToByteArray(recipeModel.Image);

                Recipe recipe = new Recipe()
                {
                    Title = recipeModel.Title,
                    Preparations = recipeModel.Preparation,
                    Ingredients = recipeModel.Ingredients
                };

                FoodImage foodImage = new FoodImage()
                {
                    Image = imageArray,
                    RecipeId = recipe.Id
                };

                recipe.FoodImage = foodImage;
                recipe.Save("1", _recipeDal, _foodImageDal);
            }
            else
            {
                ModelState.AddModelError("Image", "File upload failed");
            }
            
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}