using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Backend_AguaTracker.Domain;
using Backend_AguaTracker.Repository.Interfaces;    

namespace Backend_AguaTracker.Repository.RepositoriesClasses
{
    internal class UserRepository: IUserRepository
    {
        private readonly AguaTracker_DbContext _context;
        public UserRepository(AguaTracker_DbContext context)
        {
            _context = context;
        }

        public async Task<Result<User>> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user != null ? Result<User>.Success(user) : Result<User>.Failure("El usuario no existe.");
        }

        public async Task<Result<User>> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null ? Result<User>.Success(user) : Result<User>.Failure("El usuario no existe.");
        }

        public async Task<Result<bool>> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            return Result<bool>.Success(true);
        }

        public async Task<Result<List<User>>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return Result<List<User>>.Success(users);
        }

    }
}
