using System.Collections.Generic;

namespace LogicLayer
{
    public class RecipeContainer
    {
        private List<Recipe> _recipes = new List<Recipe>();

        public void AddRecipe(Recipe recipe)
        {
            _recipes.Add(recipe);
        }

        public void RemoveRecipe(Recipe recipe)
        {
            _recipes.Remove(recipe);
        }

        public List<Recipe> FindAllByUserId(string userId)
        {
            return _recipes.FindAll(r => r.UserId == userId);
        }

        public Recipe FindById(string recipeId)
        {
            return _recipes.Find(r => r.Id == recipeId);
        }
    }
}