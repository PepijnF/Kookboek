using System.Collections.Generic;
using AbstractionLayer;

namespace LogicLayer
{
    public class UserContainer
    {
        private readonly IUserDal _userDal;
        // TODO Check solid
        private readonly IRecipeDal _recipeDal;
        private readonly IFoodImageDal _foodImageDal;
        private readonly List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            user.Save(_userDal, _recipeDal, _foodImageDal);
            _users.Add(user);
        }

        public void RemoveUser(User user)
        {
            _users.Remove(user);
        }

        public User FindByUsername(string username)
        {
            return _users.Find(u => u.Username == username);
        }

        public User FindById(string userId)
        {
            return _users.Find(u => u.Id == userId);
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public UserContainer(IUserDal userDal, IRecipeDal recipeDal, IFoodImageDal foodImageDal, RecipeContainer recipeContainer)
        {
            _userDal = userDal;
            _recipeDal = recipeDal;
            _foodImageDal = foodImageDal;

            foreach (var userDto in _userDal.GetAll().Result)
            {
                _users.Add(new User()
                {
                    Id = userDto.Id,
                    Username = userDto.Username,
                    Password = userDto.Password,
                    Recipes = recipeContainer.FindAllByUserId(userDto.Id)
                });
            }
        }
    }
}