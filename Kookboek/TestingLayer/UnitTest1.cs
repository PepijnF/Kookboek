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
            await recipeDal.Get();
        }
    }
}