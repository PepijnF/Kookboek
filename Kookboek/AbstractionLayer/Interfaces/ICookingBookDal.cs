using System.Collections.Generic;
using System.Threading.Tasks;
using AbstractionLayer;

namespace AbstractionLayer
{
    public interface ICookingBookDal
    {
        public Task<List<CookingBookDto>> GetAllByUserId(string userId);
        public Task Save(CookingBookDto cookingBookDto, List<string> recipeIds);
        public Task<CookingBookDto> FindById(string cookingBookId);
    }
}