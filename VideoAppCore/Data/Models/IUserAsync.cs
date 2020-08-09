using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAppCore.Data.Models
{
    public interface IUserAsync
    {
        Task<User> AddUserAsync(User model);
        Task<List<User>> GetUserListAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> UpdateUserAsync(User model);
        Task RemoveUserAsync(int userId);

        Task<User> GetUserByEMail(string email);

        Task<User> LogIn(string email, string password);

    }
}
