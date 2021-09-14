using System;
using System.Threading.Tasks;

namespace AbstractionLayer
{
    public interface ISaveRecipe
    {
        public void SendRecipeToDb(Recipe recipe);
        public Task<Recipe> GetRecipeFromDb();
    }
}