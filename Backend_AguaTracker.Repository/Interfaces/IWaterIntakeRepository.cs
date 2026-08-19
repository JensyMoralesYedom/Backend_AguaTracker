using Backend_AguaTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Repository.Interfaces
{
    public interface IWaterIntakeRepository
    {
        Task AddWaterIntakeAsync(WaterIntake waterIntake);

        Task<List<WaterIntake>> GetWaterIntakesByUserIdAsync(int userId);

        Task<WaterIntake> GetWaterIntakeByIdAsync(int id);

        Task UpdateWaterIntakeAsync(WaterIntake waterIntake);

        Task DeleteWaterIntakeAsync(WaterIntake waterIntake);

        Task<List<WaterIntake>> GetAllWaterIntakesAsync();
    }
}
