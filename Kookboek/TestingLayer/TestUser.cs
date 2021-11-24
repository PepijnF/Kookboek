using AbstractionLayer;
using LogicLayer;
using NUnit.Framework;

namespace TestingLayer
{
    public class TestUser
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test(Description = "Check if a recipe can be added to the user")]
        public void AddRecipe_NewRecipe_UpdatedRecipeList()
        {
            // Arrange
            User user = new User();
            Recipe recipe = new Recipe()
            {
                Id = "1"
            };

            // Act
            user.AddRecipe(recipe);
            // Assert
            Assert.Contains(recipe, user.Recipes);
        }
    }
}