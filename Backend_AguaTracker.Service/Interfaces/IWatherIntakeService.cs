using Backend_AguaTracker.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Service.Interfaces
{
    internal interface IWatherIntakeService
    {
        Task<Result<WaterIntake>> AddWaterIntakeAsync(WaterIntake waterIntake);
        Task<Result<List<WaterIntake>>> GetWaterIntakesByUserIdAsync(int userId);
        Task<Result<WaterIntake>> GetWaterIntakeByIdAsync(int id);
        Task<Result<bool>> DeleteWaterIntakeAsync(int id);
        Task<Result<WaterIntake>> UpdateWaterIntakeAsync(WaterIntake waterIntake);

    }
}
