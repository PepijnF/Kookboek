using System.Threading.Tasks;

namespace AbstractionLayer
{
    public interface IRecipeDal
    {
        public Task Insert(Recipe recipe);
    }
}