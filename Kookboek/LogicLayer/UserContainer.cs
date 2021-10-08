using System.Collections.Generic;

namespace LogicLayer
{
    public class UserContainer
    {
        private List<User> _users = new List<User>();

        public void AddUser(User user)
        {
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
    }
}