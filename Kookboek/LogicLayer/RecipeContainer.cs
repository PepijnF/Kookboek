using System.Collections.Generic;
using AbstractionLayer;

namespace LogicLayer
{
    public class RecipeContainer
    {
        private IRecipeDal _recipeDal { get; set; }
        // TODO Check solid 
        private IFoodImageDal _foodImageDal { get; set; }
        private List<Recipe> _recipes = new List<Recipe>();

        public void AddRecipe(Recipe recipe)
        {
            recipe.Save(_recipeDal, _foodImageDal);
            _recipes.Add(recipe);
        }

        public void EditRecipe(Recipe recipe)
        {
            recipe.Save(_recipeDal, _foodImageDal);
            int index = _recipes.FindIndex(r => r.Id == recipe.Id);
            _recipes.RemoveAt(index);
            _recipes.Add(recipe);
        }

        public void RemoveRecipe(Recipe recipe)
        {
            _recipes.Remove(recipe);
        }

        public List<Recipe> FindAllByUserId(string userId)
        {
            return _recipes.FindAll(r => r.UserId == userId);
        }

        public Recipe FindById(string recipeId)
        {
            return _recipes.Find(r => r.Id == recipeId);
        }

        public List<Recipe> FindAllByCookingBookId(string cookingBookId)
        {
            List<Recipe> recipes = new List<Recipe>();
            foreach (var recipeDto in _recipeDal.GetAllByCookingBookId(cookingBookId).GetAwaiter().GetResult())
            {
                var tempImage = _foodImageDal.FindByRecipeId(recipeDto.Id).GetAwaiter().GetResult();
                                
                recipes.Add(new Recipe()
                {
                    Id = recipeDto.Id,
                    Title = recipeDto.Title,
                    Ingredients = recipeDto.Ingredients,
                    Preparations = recipeDto.Preparation,
                    UserId = recipeDto.OwnerId,
                    FoodImage = new FoodImage()
                    {
                        Id = tempImage.Id,
                        Image = tempImage.Image,
                        RecipeId = tempImage.RecipeId
                    }
                });
            }

            return recipes;
        }

        public RecipeContainer(IRecipeDal recipeDal, IFoodImageDal foodImageDal)
        {
            _recipeDal = recipeDal;
            _foodImageDal = foodImageDal;
            
            foreach (var recipDto in _recipeDal.GetAll().Result)
            {
                var tempImage = foodImageDal.FindByRecipeId(recipDto.Id).GetAwaiter().GetResult();
                
                _recipes.Add(new Recipe()
                {
                    Id = recipDto.Id,
                    Title = recipDto.Title,
                    Ingredients = recipDto.Ingredients,
                    Preparations = recipDto.Preparation,
                    UserId = recipDto.OwnerId,
                    FoodImage = new FoodImage()
                    {
                        Id = tempImage.Id,
                        Image = tempImage.Image,
                        RecipeId = tempImage.RecipeId
                    }
                });
            }
        }
    }
}