using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend_AguaTracker.Domain;
using Backend_AguaTracker.Repository.Interfaces;
using Backend_AguaTracker.Service.Interfaces;

namespace Backend_AguaTracker.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<User>> GetUserByIdAsync(int id)
        {
            var result = await _unitOfWork.Users.GetUserByIdAsync(id);
            if (!result.IsSuccess)
            {
                return Result<User>.Failure(result.Error);
            }

            return Result<User>.Success(result.Value);
        }

        public async Task<Result<User>> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<User>.Failure("El correo electrónico no puede estar vacío.");
            }

            var result = await _unitOfWork.Users.GetUserByEmailAsync(email);
            if (!result.IsSuccess)
            {
                return Result<User>.Failure(result.Error);
            }

            return Result<User>.Success(result.Value);
        }

        public async Task<Result<List<User>>> GetAllUsersAsync()
        {
            var result = await _unitOfWork.Users.GetAllUsersAsync();
            if (!result.IsSuccess)
            {
                return Result<List<User>>.Failure(result.Error);
            }

            return Result<List<User>>.Success(result.Value);
        }

        public async Task<Result<User>> AddUserAsync(User user)
        {
            if (user == null)
            {
                return Result<User>.Failure("Los datos del usuario no pueden ser nulos.");
            }

            if (user.Weight <= 0)
            {
                return Result<User>.Failure("El peso del usuario debe ser mayor a cero.");
            }

            var existingUser = await _unitOfWork.Users.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return Result<User>.Failure("Ya existe un usuario registrado con este correo electrónico.");
            }

            await _unitOfWork.Users.AddUserAsync(user);
            await _unitOfWork.CompleteAsync();
            return Result<User>.Success(user);
        }

        public async Task<Result<bool>> UpdateUserAsync(User user)
        {
            if (user == null)
            {
                return Result<bool>.Failure("Los datos del usuario no pueden ser nulos.");
            }

            var existingUser = await _unitOfWork.Users.GetUserByIdAsync(user.Id);
            if (existingUser == null)
            {
                return Result<bool>.Failure("No se puede actualizar: el usuario no existe.");
            }

            await _unitOfWork.Users.UpdateUserAsync(user);
            await _unitOfWork.CompleteAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteUserAsync(User user)
        {
            if (user == null)
            {
                return Result<bool>.Failure("El usuario a eliminar no puede ser nulo.");
            }

            var existingUser = await _unitOfWork.Users.GetUserByIdAsync(user.Id);
            if (existingUser == null)
            {
                return Result<bool>.Failure("No se puede eliminar: el usuario no existe.");
            }

            await _unitOfWork.Users.DeleteUserAsync(user);
            await _unitOfWork.CompleteAsync();
            return Result<bool>.Success(true);
        }
    }
}