using System.Collections.Generic;
using System.Threading.Tasks;
using AbstractionLayer;
using LogicLayer;
using NUnit.Framework;
using Moq;
using User = LogicLayer.User;

namespace TestingLayer
{
    public class TestUser
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void AddRecipeToUser()
        {
            // Arrange
            User user = new User()
            {
                Username = "Test",
                Password = "test123"
            };

            Recipe recipe = new Recipe()
            {
                Title = "TestRecipe",
                Preparations = "Test",
                Ingredients = "Test"
            };
            
            // Act
            user.AddRecipe(recipe);
            
            // Assert
            Assert.Contains(recipe, user.Recipes);
        }

        [Test]
        public void RetrieveUserByUsername()
        {
            // Arrange
            UserDto userDto = new UserDto();
            
            var mockUserDal = new Mock<IUserDal>();
            Task<UserDto> userTask = Task.Run(() => userDto);
            mockUserDal.Setup(x => x.FindByUsername("testUser")).Returns(userTask);
            //mockUserDal.Setup(x => x.FindByUsername(It.IsAny<string>())).Returns();


            List<RecipeDto> recipes = new List<RecipeDto>();
            recipes.Add(new RecipeDto()
            {
                Title = "TestTitle"
            });
            var mockRecipeDal = new Mock<IRecipeDal>();
            Task<List<RecipeDto>> recipeTask = Task.Run(() => recipes);
            mockRecipeDal.Setup(x => x.FindAllByUserId(It.IsAny<string>())).Returns(recipeTask);
            

            var mockImageDal = new Mock<IFoodImageDal>();
            FoodImageDto foodImageDto = new FoodImageDto()
            {
                Id = "Test"
            };
            Task<FoodImageDto> imageTask = Task.Run(() => foodImageDto);
            mockImageDal.Setup(x => x.FindByRecipeId(It.IsAny<string>())).Returns(imageTask);

            // Act
            User user = new User("testUser", mockUserDal.Object, mockRecipeDal.Object, mockImageDal.Object);
            
            // Assert
            Assert.AreEqual(recipes[0].Title,user.Recipes[0].Title);
            Assert.AreEqual(foodImageDto.Id, user.Recipes[0].FoodImage.Id);
        }
    }
}