using Backend_AguaTracker.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Backend_AguaTracker.Repository.Interfaces;

namespace Backend_AguaTracker.Service.Services
{
    public class DailyResumeService
    {
        private readonly IDailyResumenRepository _dailyResumenRepository;
        private readonly IUserRepository _userRepository;

        public DailyResumeService(IDailyResumenRepository dailyResumenRepository, IUserRepository userRepository)
        {
            _dailyResumenRepository = dailyResumenRepository;
            _userRepository = userRepository;
        }

        public async Task<DailyResume> GetDailyResumenByDateAsync(DateTime date)
        {
            return await _dailyResumenRepository.GetDailyResumenByDateAsync(date);
        }

        public async Task<DailyResume> GetDailyResumenByUserIdAndDateAsync(int userId, DateTime date)
        {
            return await _dailyResumenRepository.GetDailyResumenByUserIdAndDateAsync(userId, date);
        }

        public async Task<List<DailyResume>> GetDailyResumenByUserIdAsync(int userId)
        {
            return await _dailyResumenRepository.GetDailyResumenByUserIdAsync(userId);
        }

        public async Task<List<DailyResume>> GetAllDailyResumenAsync()
        {
            return await _dailyResumenRepository.GetAllDailyResumenAsync();
        }

        public async Task<DailyResume> GetDailyResumenByIdAsync(int id)
        {
            return await _dailyResumenRepository.GetDailyResumenByIdAsync(id);
        }

        public async Task DeleteDailyResumenAsync(DailyResume dailyResumen)
        {
            await _dailyResumenRepository.DeleteDailyResumenAsync(dailyResumen);
        }

        public async Task UpdateDailyResumenAsync(DailyResume dailyResumen)
        {
            await _dailyResumenRepository.UpdateDailyResumenAsync(dailyResumen);
        }

        public async Task<Result<DailyResume>> AddDailyResumenAsync(int userId, DateTime date, int intaketotal, ActivityLevelEnum? activity)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null) {
               return Result<DailyResume>.Failure("el usuario no existe");
            }

            ActivityLevelEnum activityLevel = activity ?? user.DefaultActivityLevel;

            int intakeGoal = CalculateIntakeGoal((int)user.Weight, activityLevel);
            var dailyResumen = new DailyResume
            {
                UserId = userId,
                Date = date,
                TotalIntake = intaketotal,
                IntakeGoal = intakeGoal,
                ActivityLevel = activityLevel
            };
            await _dailyResumenRepository.AddDailyResumenAsync(dailyResumen);

            return Result<DailyResume>.Success(dailyResumen);
        }

        public int CalculateIntakeGoal(int weight, ActivityLevelEnum activityLevel)
        {
            return (weight * 33) + (int)activityLevel;
        }
    }
}
