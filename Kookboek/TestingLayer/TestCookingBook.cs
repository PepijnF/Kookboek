using AbstractionLayer;
using LogicLayer;
using Moq;
using NUnit.Framework;

namespace TestingLayer
{
    public class TestCookingBook
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test(Description = "Check if a new recipe can be added to the cooking book")]
        public void AddRecipe_NewRecipe_UpdatedRecipeList()
        {
            // Arrange
            var mockCookingBookDal = new Mock<ICookingBookDal>();
            
            CookingBook cookingBook = new CookingBook(mockCookingBookDal.Object);

            Recipe recipe = new Recipe()
            {
                Id = "1"
            };
            
            // Act
            cookingBook.AddRecipe(recipe);
            
            // Assert
            Assert.Contains(recipe ,cookingBook.Recipes);
        }
    }
}