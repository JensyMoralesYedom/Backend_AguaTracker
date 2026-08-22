using Backend_AguaTracker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Service.Interfaces
{
    public interface IDailyResumeService
    {
        Task<Result<DailyResume>> GetDailyResumenByDateAsync(DateTime date);
        Task<Result<DailyResume>> GetDailyResumenByUserIdAndDateAsync(int userId, DateTime date);
        Task<Result<List<DailyResume>>> GetDailyResumenByUserIdAsync(int userId);
        Task<Result<List<DailyResume>>> GetAllDailyResumenAsync();
        Task<Result<DailyResume>> GetDailyResumenByIdAsync(int id);
        Task<Result<bool>> DeleteDailyResumenAsync(DailyResume dailyResumen);
        Task<Result<bool>> UpdateDailyResumenAsync(DailyResume dailyResumen);
        Task<Result<DailyResume>> AddDailyResumenAsync(int userId, DateTime date, int intaketotal, ActivityLevelEnum? activity);
    }
}
