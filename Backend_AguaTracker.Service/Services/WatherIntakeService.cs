using System;
using System.Collections.Generic;
using System.Text;
using Backend_AguaTracker.Domain;
using Backend_AguaTracker.Repository.Interfaces;
using Backend_AguaTracker.Service.Interfaces;

namespace Backend_AguaTracker.Service.Services
{
    public class WatherIntakeService : IWatherIntakeService
    {
        private readonly IWaterIntakeRepository _waterIntakeRepository;

        public WatherIntakeService(IWaterIntakeRepository waterIntakeRepository)
        {
            _waterIntakeRepository = waterIntakeRepository;
        }

        public async Task<Result<WaterIntake>> AddWaterIntakeAsync(WaterIntake waterIntake)
        {
            if (waterIntake == null)
            {
                return Result<WaterIntake>.Failure("Los datos de la ingesta de agua no pueden ser nulos.");
            }
            if (waterIntake.Amount <= 0)
            {
                return Result<WaterIntake>.Failure("La cantidad de agua debe ser mayor a cero.");
            }

            await _waterIntakeRepository.AddWaterIntakeAsync(waterIntake);
            return Result<WaterIntake>.Success(waterIntake);
        }

        public async Task<Result<List<WaterIntake>>> GetWaterIntakesByUserIdAsync(int userId)
        {
            var result = await _waterIntakeRepository.GetWaterIntakesByUserIdAsync(userId);
            return result;
        }

        public async Task<Result<WaterIntake>> GetWaterIntakeByIdAsync(int id)
        {
            var result = await _waterIntakeRepository.GetWaterIntakeByIdAsync(id);
            if (result.Value == null)
            {
                return Result<WaterIntake>.Failure($"Ingesta de agua con ID {id} no encontrada.");
            }
            return result;
        }

        public async Task<Result<bool>> DeleteWaterIntakeAsync(int id)
        {
            var result = await _waterIntakeRepository.GetWaterIntakeByIdAsync(id);
            var waterIntake = result.Value;

            if (waterIntake == null)
            {
                return Result<bool>.Failure($"Ingesta de agua con ID {id} no encontrada.");
            }

            return await _waterIntakeRepository.DeleteWaterIntakeAsync(waterIntake);
        }

        public async Task<Result<WaterIntake>> UpdateWaterIntakeAsync(WaterIntake waterIntake)
        {
            if (waterIntake == null)
            {
                return Result<WaterIntake>.Failure("Los datos de la ingesta de agua no pueden ser nulos.");
            }
            if (waterIntake.Amount <= 0)
            {
                return Result<WaterIntake>.Failure("La cantidad de agua debe ser mayor a cero.");
            }
            var existingWaterIntake = await _waterIntakeRepository.GetWaterIntakeByIdAsync(waterIntake.Id);
            if (existingWaterIntake == null)
            {
                return Result<WaterIntake>.Failure($"Ingesta de agua con ID {waterIntake.Id} no encontrada.");
            }
            await _waterIntakeRepository.UpdateWaterIntakeAsync(waterIntake);
            return Result<WaterIntake>.Success(waterIntake);
        }
    }
}
