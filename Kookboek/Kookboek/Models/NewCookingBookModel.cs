using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kookboek.Models
{
    public class NewCookingBookModel
    {
        public CookingBookModel CookingBookModel { get; set; }
        public IEnumerable<SelectListItem> RecipeModels;
        public IEnumerable<string> SelectedRecipes;
    }
}