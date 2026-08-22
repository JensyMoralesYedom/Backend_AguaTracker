using Backend_AguaTracker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Service.Interfaces
{
    public interface IWaterIntakeService
    {
        Task<Result<WaterIntake>> AddWaterIntakeAsync(int userId, int amounthMl, DateTime date);
        Task<Result<List<WaterIntake>>> GetWaterIntakesByUserIdAsync(int userId);
        Task<Result<WaterIntake>> GetWaterIntakeByIdAsync(int id);
        Task<Result<bool>> DeleteWaterIntakeAsync(int id);
        Task<Result<WaterIntake>> UpdateWaterIntakeAsync(WaterIntake waterIntake);

    }
}
