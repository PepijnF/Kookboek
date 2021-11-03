using System.Collections.Generic;
using AbstractionLayer;

namespace LogicLayer
{
    public class CookingBookContainer
    {
        private RecipeContainer _recipeContainer;
        private ICookingBookDal _cookingBookDal;
        private List<CookingBook> _cookingBooks = new List<CookingBook>();

        public CookingBookContainer(RecipeContainer recipeContainer, ICookingBookDal cookingBookDal)
        {
            _recipeContainer = recipeContainer;
            _cookingBookDal = cookingBookDal;
        }

        public List<CookingBook> GetAllByUserId(string userId)
        {
            List<CookingBook> cookingBooks = new List<CookingBook>();
            foreach (var cookingBookDto in _cookingBookDal.GetAllByUserId(userId).GetAwaiter().GetResult())
            {
                cookingBooks.Add(new CookingBook(cookingBookDto, _recipeContainer.FindAllByCookingBookId(cookingBookDto.Id)));
            }

            return cookingBooks;

        }

        public CookingBook FindById(string cookingBookId)
        {
            var cookingBookDto = _cookingBookDal.FindById(cookingBookId).GetAwaiter().GetResult();
            List<Recipe> recipes = _recipeContainer.FindAllByCookingBookId(cookingBookDto.Id);
            return new CookingBook(cookingBookDto, recipes);
        }
    }
}