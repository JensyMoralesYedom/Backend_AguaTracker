using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;
using Backend_AguaTracker.Domain;
using Backend_AguaTracker.Repository.Interfaces;
using Backend_AguaTracker.Service.Interfaces;

namespace Backend_AguaTracker.Service.Services
{
    public class WaterIntakeService : IWaterIntakeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WaterIntakeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<WaterIntake>> AddWaterIntakeAsync(int userId, int amounthMl, DateTime date)
        {
            var result = await _unitOfWork.DailyResumes.
                                        GetDailyResumenByUserIdAndDateAsync(userId, date);

            if(!result.IsSuccess)
            {
                return Result<WaterIntake>.Failure($"No se encontró un resumen diario para el usuario con ID {userId} y fecha {date}");
            }

            var dailyResume = result.Value;

            var waterIntake = new WaterIntake
            {
                DailyResumenId = dailyResume.Id,
                Amount = amounthMl,
         
            };

            var resp = await _unitOfWork.WaterIntakes.AddWaterIntakeAsync(waterIntake);

            if(!resp.IsSuccess)
            {
                return Result<WaterIntake>.Failure($"Error al agregar la ingesta de agua: {resp.Error}");
            }

            dailyResume.TotalIntake += amounthMl;
            
            await _unitOfWork.DailyResumes.UpdateDailyResumenAsync(dailyResume);

            await _unitOfWork.CompleteAsync();
            return Result<WaterIntake>.Success(waterIntake);
        }

        public async Task<Result<List<WaterIntake>>> GetWaterIntakesByUserIdAsync(int userId)
        {
            var result = await _unitOfWork.WaterIntakes.GetWaterIntakesByUserIdAsync(userId);
            return result;
        }

        public async Task<Result<WaterIntake>> GetWaterIntakeByIdAsync(int id)
        {
            var result = await _unitOfWork.WaterIntakes.GetWaterIntakeByIdAsync(id);
            if (result.Value == null)
            {
                return Result<WaterIntake>.Failure($"Ingesta de agua con ID {id} no encontrada.");
            }
            return result;
        }

        public async Task<Result<bool>> DeleteWaterIntakeAsync(int id)
        {
            var result = await _unitOfWork.WaterIntakes.GetWaterIntakeByIdAsync(id);
            var waterIntake = result.Value;

            if (waterIntake == null)
            {
                return Result<bool>.Failure($"Ingesta de agua con ID {id} no encontrada.");
            }

            return await _unitOfWork.WaterIntakes.DeleteWaterIntakeAsync(waterIntake);
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
            var existingWaterIntake = await _unitOfWork.WaterIntakes.GetWaterIntakeByIdAsync(waterIntake.Id);
            if (existingWaterIntake == null)
            {
                return Result<WaterIntake>.Failure($"Ingesta de agua con ID {waterIntake.Id} no encontrada.");
            }
            await _unitOfWork.WaterIntakes.UpdateWaterIntakeAsync(waterIntake);
            return Result<WaterIntake>.Success(waterIntake);
        }
    }
}
