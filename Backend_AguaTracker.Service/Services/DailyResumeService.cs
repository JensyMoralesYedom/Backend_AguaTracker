using Backend_AguaTracker.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend_AguaTracker.Repository.Interfaces;
using Backend_AguaTracker.Service.Interfaces;

namespace Backend_AguaTracker.Service.Services
{
    public class DailyResumeService : IDailyResumeService
    {
        private readonly IDailyResumenRepository _dailyResumenRepository;
        private readonly IUserRepository _userRepository;

        public DailyResumeService(IDailyResumenRepository dailyResumenRepository, IUserRepository userRepository)
        {
            _dailyResumenRepository = dailyResumenRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<DailyResume>> GetDailyResumenByDateAsync(DateTime date)
        {
            var result = await _dailyResumenRepository.GetDailyResumenByDateAsync(date);
            if (!result.IsSuccess)
            {
                return Result<DailyResume>.Failure(result.Error);
            }
            return result;
        }

        public async Task<Result<DailyResume>> GetDailyResumenByUserIdAndDateAsync(int userId, DateTime date)
        {
            var result = await _dailyResumenRepository.GetDailyResumenByUserIdAndDateAsync(userId, date);
            if (!result.IsSuccess)
            {
                return Result<DailyResume>.Failure(result.Error);
            }
            return result;
        }

        public async Task<Result<List<DailyResume>>> GetDailyResumenByUserIdAsync(int userId)
        {
            var result = await _dailyResumenRepository.GetDailyResumenByUserIdAsync(userId);
            return result;
        }

        public async Task<Result<List<DailyResume>>> GetAllDailyResumenAsync()
        {
            var result = await _dailyResumenRepository.GetAllDailyResumenAsync();
            return result;
        }

        public async Task<Result<DailyResume>> GetDailyResumenByIdAsync(int id)
        {
            var result = await _dailyResumenRepository.GetDailyResumenByIdAsync(id);
            if (!result.IsSuccess)
            {
                return Result<DailyResume>.Failure(result.Error);
            }
            return result;
        }

        public async Task<Result<bool>> DeleteDailyResumenAsync(DailyResume dailyResumen)
        {
            if (dailyResumen == null)
            {
                return Result<bool>.Failure("El registro a eliminar no puede ser nulo.");
            }

            await _dailyResumenRepository.DeleteDailyResumenAsync(dailyResumen);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateDailyResumenAsync(DailyResume dailyResumen)
        {
            if (dailyResumen == null)
            {
                return Result<bool>.Failure("El registro a actualizar no puede ser nulo.");
            }

            await _dailyResumenRepository.UpdateDailyResumenAsync(dailyResumen);
            return Result<bool>.Success(true);
        }

        public async Task<Result<DailyResume>> AddDailyResumenAsync(int userId, DateTime date, int intaketotal, ActivityLevelEnum? activity)
        {
            var result = await _userRepository.GetUserByIdAsync(userId);
            var user = result.IsSuccess ? result.Value : null;

            if (user == null)
            {
                return Result<DailyResume>.Failure("El usuario no existe.");
            }

            // Lógica de fallback aplicada correctamente
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
