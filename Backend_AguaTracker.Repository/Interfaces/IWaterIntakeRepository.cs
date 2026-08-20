using Backend_AguaTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Repository.Interfaces
{
    public interface IWaterIntakeRepository
    {
        Task<Result<bool>> AddWaterIntakeAsync(WaterIntake waterIntake);
        Task<Result<List<WaterIntake>>> GetWaterIntakesByUserIdAsync(int userId);
        Task<Result<WaterIntake>> GetWaterIntakeByIdAsync(int id);
        Task<Result<bool>> UpdateWaterIntakeAsync(WaterIntake waterIntake);
        Task<Result<bool>> DeleteWaterIntakeAsync(WaterIntake waterIntake);
        Task<Result<List<WaterIntake>>> GetAllWaterIntakesAsync();
    }
}
