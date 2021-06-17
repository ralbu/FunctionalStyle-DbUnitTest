using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class UserRepository
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public void Add(User user)
        {
            _userContext.Add(user);
            _userContext.SaveChanges();
        }

        public IList<User> GetUsers()
            => _userContext.User.ToList();
    }
}