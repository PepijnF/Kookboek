using System.Threading.Tasks;

namespace AbstractionLayer
{
    public interface IUserDal
    {
        public Task<UserDto> FindByUsername(string username);
        public Task<UserDto> FindById(string userId);
        public Task Save(UserDto userDto);
    }
}