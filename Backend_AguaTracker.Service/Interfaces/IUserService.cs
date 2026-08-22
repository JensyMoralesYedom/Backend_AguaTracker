using Backend_AguaTracker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Service.Interfaces
{
    public interface IUserService
    {
        Task<Result<User>> GetUserByIdAsync(int id);
        Task<Result<User>> GetUserByEmailAsync(string email);
        Task<Result<List<User>>> GetAllUsersAsync();
        Task<Result<User>> AddUserAsync(User user);
        Task<Result<bool>> UpdateUserAsync(User user);
        Task<Result<bool>> DeleteUserAsync(User user);
    }
}
