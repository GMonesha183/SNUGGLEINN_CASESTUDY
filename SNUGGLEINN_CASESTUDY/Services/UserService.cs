using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return _userRepository.GetAllUsersAsync();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<User> AddUserAsync(User user)
        {
            // extra logic can be added here later
            return _userRepository.AddUserAsync(user);
        }

        public Task<User> UpdateUserAsync(User user)
        {
            return _userRepository.UpdateUserAsync(user);
        }

        public Task<bool> DeleteUserAsync(int id)
        {
            return _userRepository.DeleteAsync(id);
        }

        public Task<User> AuthenticateAsync(string email, string password)
        {
            return _userRepository.AuthenticateAsync(email, password);
        }
    }
}
