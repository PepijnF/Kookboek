using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AbstractionLayer
{
    public interface IRecipeLogic
    {
        public void SendRecipeToDb(byte[] image, string title, string ingredients, string preparation);
        public Task<List<RecipeDto>> GetRecipeFromDb();
        public byte[] FormFileToByteArray(IFormFile file);
    }
}