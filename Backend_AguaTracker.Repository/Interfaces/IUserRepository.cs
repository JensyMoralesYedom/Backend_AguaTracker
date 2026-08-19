using Backend_AguaTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByEmailAsync(string email);

        Task AddUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(User user);

        Task<List<User>> GetAllUsersAsync();

    }
}
