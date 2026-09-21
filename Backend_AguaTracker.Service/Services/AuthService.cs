using System;
using System.Collections.Generic;
using System.Text;
using Backend_AguaTracker.Domain;
using Backend_AguaTracker.Service.Interfaces;
using Backend_AguaTracker.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;

namespace Backend_AguaTracker.Service.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;


        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<string> CreateToken(User user)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var jwt = new Jwt
            {
                key = jwtSection["key"],
                Issuer = jwtSection["Issuer"],
                Audience = jwtSection["Audience"],
                Subject = jwtSection["Subject"]
            };

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.Email)

            };

            var key = new Microsoft.IdentityModel.Tokens.
                SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));

            var creds = new Microsoft.IdentityModel.Tokens.
                SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Result<string>> LoginAsync(string email, string password)
        {
            // Check if the user exists
            var result = await _unitOfWork.Users.GetUserByEmailAsync(email);

            if(!result.IsSuccess)
            {
                return Result<string>.Failure("User not found");
            }

            var user = result.Value;

            if (user == null)
            {
                return Result<string>.Failure("User not found");
            }

            // Check if the password is correct
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return Result<string>.Failure("Invalid password");
            }

            // Generate a JWT token
            var token = await CreateToken(user);

            return Result<string>.Success(token);
        }

        public async Task<Result<string>> RegisterAsync(User user)
        {
            var existinUserEmail = await _unitOfWork.Users.GetUserByEmailAsync(user.Email);
            if(existinUserEmail.IsSuccess)
            {
                return Result<string>.Failure("El email del usuario ya existe");
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            
            var result = await _unitOfWork.Users.AddUserAsync(user);
            if (!result.IsSuccess)
            {
                return Result<string>.Failure(result.Error);
            }

            await _unitOfWork.CompleteAsync();

            var token = await CreateToken(user);

            return Result<string>.Success(token);
        }
    }
}
