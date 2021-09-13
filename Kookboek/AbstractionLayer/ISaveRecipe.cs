using System;

namespace AbstractionLayer
{
    public interface ISaveRecipe
    {
        public void SendRecipeToDb(Recipe recipe);
    }
}