using Backend_AguaTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Repository.Interfaces
{
    public interface IDailyResumenRepository
    {
        Task<DailyResume> GetDailyResumenByDateAsync(DateTime date);

        Task<DailyResume> GetDailyResumenByUserIdAndDateAsync(int userId, DateTime date);

        Task<List<DailyResume>> GetDailyResumenByUserIdAsync(int userId);

        Task<List<DailyResume>> GetAllDailyResumenAsync();

        Task<DailyResume> GetDailyResumenByIdAsync(int id);

        Task DeleteDailyResumenAsync(DailyResume dailyResumen);

        Task UpdateDailyResumenAsync(DailyResume dailyResumen);

        Task AddDailyResumenAsync(DailyResume dailyResumen);
    }
}
