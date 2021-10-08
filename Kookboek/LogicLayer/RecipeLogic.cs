using System;
using System.Collections.Generic;
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

        public void SendRecipeToDb(byte[] image, string title, string ingredients, string preparation)
        {
            RecipeDto recipeDto = new RecipeDto()
            {
                Title = title,
                Ingredients = ingredients,
                Preparation = preparation
            };
            
            _recipeDal.Save(recipeDto);
        }

        public byte[] FormFileToByteArray(IFormFile file)
        {
            long fileLength = file.Length;
            using Stream fileStream = file.OpenReadStream();
            byte[] byteFile = new byte[fileLength];
            fileStream.Read(byteFile, 0, (int) file.Length);

            return byteFile;
        }

        public async Task<List<RecipeDto>> GetRecipeFromDb()
        {
            List<RecipeDto> recipeDto = await _recipeDal.FindAllByUserId("asd");
            return recipeDto;
        }
    }
}