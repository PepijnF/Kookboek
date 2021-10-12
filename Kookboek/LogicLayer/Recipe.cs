using AbstractionLayer;

namespace LogicLayer
{
    public class Recipe
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Preparations { get; set; }
        public string Ingredients { get; set; }
        public FoodImage FoodImage { get; set; }

        public void Save(IRecipeDal recipeDal, IFoodImageDal foodImageDal)
        {
            recipeDal.Save(new RecipeDto()
            {
                Id = Id,
                Title = Title,
                Preparation = Preparations,
                Ingredients = Ingredients,
                OwnerId = UserId
            }).GetAwaiter();
            FoodImage.Save(foodImageDal);
        }

        public Recipe(RecipeDto recipeDto, IFoodImageDal foodImageDal)
        {
            Id = recipeDto.Id;
            Title = recipeDto.Title;
            Preparations = recipeDto.Preparation;
            Ingredients = recipeDto.Ingredients;
            FoodImage = FoodImage.FindByRecipeId(recipeDto.Id, foodImageDal).Result;
        }

        public Recipe()
        {
            Id = System.Guid.NewGuid().ToString();
        }
    }
}
