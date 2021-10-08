using System.Collections.Generic;
using System.Threading.Tasks;
using AbstractionLayer;
using NUnit.Framework;
using DataLayer;

namespace TestingLayer
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            IRecipeDal recipeDal = new RecipeDal();
            List<RecipeDto> list = await recipeDal.FindAllByUserId("1");
        }

        [Test]
        public async Task Test2()
        {
            IRecipeDal recipeDal = new RecipeDal();
            await recipeDal.Save(new RecipeDto()
            {
                Title = "test"
            });
        }
    }
}