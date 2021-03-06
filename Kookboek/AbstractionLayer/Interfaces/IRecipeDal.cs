using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbstractionLayer
{
    public interface IRecipeDal
    {
        public Task Save(RecipeDto recipeDto);
        public Task<List<RecipeDto>> FindAllByUserId(string userId);
        public Task<RecipeDto> FindById(string recipeId);
        public Task<List<RecipeDto>> GetAll();
        public Task<List<RecipeDto>> GetAllByCookingBookId(string cookingBookId);
    }
}