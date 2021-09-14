using System.Threading.Tasks;

namespace AbstractionLayer
{
    public interface IRecipeDal
    {
        public Task Insert(Recipe recipe);
        public Task<Recipe> Get();
    }
}