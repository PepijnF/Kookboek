using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AbstractionLayer
{
    public interface IRecipeLogic
    {
        public void SendRecipeToDb(Recipe recipe);
        public Task<Recipe> GetRecipeFromDb();

        public Recipe RecipeModelToDto(IFormFile file, string title, string ingredients, string preparation);
    }
}