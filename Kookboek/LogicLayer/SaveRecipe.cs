using System;
using AbstractionLayer;

namespace LogicLayer
{
    public class SaveRecipe: ISaveRecipe
    {
        private readonly IRecipeDal _recipeDal;
        
        public SaveRecipe(IRecipeDal recipeDal)
        {
            _recipeDal = recipeDal;
        }
        public void SendRecipeToDb(Recipe recipe)
        {
            _recipeDal.Insert(recipe);
        }
    }
}