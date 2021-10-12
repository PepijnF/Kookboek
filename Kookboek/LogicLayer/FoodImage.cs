using System;
using System.Threading.Tasks;
using AbstractionLayer;

namespace LogicLayer
{
    public class FoodImage
    {
        public string Id { get; set; }
        public string RecipeId { get; set; }
        public byte[] Image { get; set; }

        public void Save(IFoodImageDal foodImageDal)
        {
            foodImageDal.Save(new FoodImageDto()
            {
                Id = Id,
                Image = Image,
                RecipeId = RecipeId
            });
        }
        public FoodImage(FoodImageDto foodImageDto)
        {
            Id = foodImageDto.Id;
            Image = foodImageDto.Image;
            RecipeId = foodImageDto.RecipeId;
        }

        public FoodImage()
        {
            Id = System.Guid.NewGuid().ToString();
        }

        public static async Task<FoodImage> FindByRecipeId(string recipeId, IFoodImageDal foodImageDal)
        {
            FoodImageDto foodImageDto = await foodImageDal.FindByRecipeId(recipeId);
            return new FoodImage(foodImageDto);
        }

        public string Base64Image()
        {
            return Convert.ToBase64String(this.Image);
        }
    }
}