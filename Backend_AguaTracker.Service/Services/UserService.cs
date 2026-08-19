using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend_AguaTracker.Domain;
using Backend_AguaTracker.Repository.Interfaces;

namespace Backend_AguaTracker.Service.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return Result<User>.Failure($"Usuario con ID {id} no encontrado.");
            }

            return Result<User>.Success(user);
        }

        public async Task<Result<User>> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<User>.Failure("El correo electrónico no puede estar vacío.");
            }

            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return Result<User>.Failure($"No existe ningún usuario registrado con el correo {email}.");
            }

            return Result<User>.Success(user);
        }

        public async Task<Result<List<User>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Result<List<User>>.Success(users);
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

            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return Result<User>.Failure("Ya existe un usuario registrado con este correo electrónico.");
            }

            await _userRepository.AddUserAsync(user);
            return Result<User>.Success(user);
        }

        public async Task<Result<bool>> UpdateUserAsync(User user)
        {
            if (user == null)
            {
                return Result<bool>.Failure("Los datos del usuario no pueden ser nulos.");
            }

            var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
            if (existingUser == null)
            {
                return Result<bool>.Failure("No se puede actualizar: el usuario no existe.");
            }

            await _userRepository.UpdateUserAsync(user);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteUserAsync(User user)
        {
            if (user == null)
            {
                return Result<bool>.Failure("El usuario a eliminar no puede ser nulo.");
            }

            var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
            if (existingUser == null)
            {
                return Result<bool>.Failure("No se puede eliminar: el usuario no existe.");
            }

            await _userRepository.DeleteUserAsync(user);
            return Result<bool>.Success(true);
        }
    }
}