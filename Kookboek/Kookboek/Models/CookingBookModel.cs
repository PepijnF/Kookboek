using System.Collections.Generic;

namespace Kookboek.Models
{
    public class CookingBookModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> RecipeIds { get; set; }
        public List<RecipeModel> RecipeModels { get; set; }
    }
}