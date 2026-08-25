using Backend_AguaTracker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> CreateToken(User user);
        Task<Result<string>> LoginAsync(string email, string password);
        Task<Result<string>> RegisterAsync(User user);
    }
}
