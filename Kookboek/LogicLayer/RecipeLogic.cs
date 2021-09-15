using System;
using System.IO;
using System.Threading.Tasks;
using AbstractionLayer;
using Microsoft.AspNetCore.Http;

namespace LogicLayer
{
    public class RecipeLogicLogic: IRecipeLogic
    {
        private readonly IRecipeDal _recipeDal;
        
        public RecipeLogicLogic(IRecipeDal recipeDal)
        {
            _recipeDal = recipeDal;
        }

        public void SendRecipeToDb(Recipe recipe)
        {
            _recipeDal.Insert(recipe);
        }
        
        public Recipe RecipeModelToDto(IFormFile file, string title, string ingredients, string preparation)
        {
            long fileLength = file.Length;
            using Stream fileStream = file.OpenReadStream();
            byte[] byteImage = new byte[fileLength];
            fileStream.Read(byteImage, 0, (int) file.Length);
            
            Recipe recipe = new Recipe()
            {
                Image = byteImage,
                Ingredients = ingredients,
                Preparation = preparation,
                Title = title
            };
            
            return recipe;
        }

        public async Task<Recipe> GetRecipeFromDb()
        {
            return await _recipeDal.Get();
        }
    }
}