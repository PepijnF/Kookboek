using System.Threading.Tasks;

namespace AbstractionLayer
{
    public interface IFoodImageDal
    {
        public Task Save(FoodImageDto foodImageDto);
        public Task<FoodImageDto> FindById(string foodImageId);
        public Task<FoodImageDto> FindByRecipeId(string recipeId);
        public Task RemoveByRecipeId(string imageId);
    }
}