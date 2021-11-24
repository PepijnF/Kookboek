using System;
using System.Collections.Generic;
using AbstractionLayer;
using LogicLayer;
using NUnit.Framework;
using Moq;

namespace TestingLayer
{
    public class TestRecipe
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test(Description = "Check if recipe can handle a null reference")]
        public void Save_EditedRecipe_NullReference()
        {
            // Arrange
            var mockRecipeDal = new Mock<IRecipeDal>();
            mockRecipeDal.Setup(r => r.Save(It.Is<RecipeDto>(r => r.Id == "1")));

            Recipe recipe = new Recipe()
            {
                Id = "2",
                Ingredients = "Apple"
            };
            
            // Act, Assert
            Assert.Throws(typeof(NullReferenceException),delegate() { recipe.Save(mockRecipeDal.Object, new Mock<IFoodImageDal>().Object); });
        }
    }
}