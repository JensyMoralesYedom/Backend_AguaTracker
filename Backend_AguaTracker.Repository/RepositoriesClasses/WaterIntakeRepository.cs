using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Backend_AguaTracker.Domain;
using Backend_AguaTracker.Repository.Interfaces;

namespace Backend_AguaTracker.Repository.RepositoriesClasses
{
    internal class WaterIntakeRepository : IWaterIntakeRepository
    {
        private readonly AguaTracker_DbContext _context;

        public WaterIntakeRepository(AguaTracker_DbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> AddWaterIntakeAsync(WaterIntake waterIntake)
        {
            _context.WaterIntakes.Add(waterIntake);
            return Result<bool>.Success(true);
        }

        public async Task<Result<List<WaterIntake>>> GetWaterIntakesByUserIdAsync(int userId)
        {
            var waterIntakes = await _context.WaterIntakes
                .AsNoTracking()
                .Include(wi => wi.DailyResumen)
                .Where(wi => wi.DailyResumen.UserId == userId)
                .ToListAsync();
            return Result<List<WaterIntake>>.Success(waterIntakes);
        }

        public async Task<Result<WaterIntake>> GetWaterIntakeByIdAsync(int id)
        {
            var waterIntake = await _context.WaterIntakes.FirstOrDefaultAsync(wi => wi.Id == id);
            if (waterIntake == null)
            {
                return Result<WaterIntake>.Failure($"Ingesta de agua con ID {id} no encontrada.");
            }
            return Result<WaterIntake>.Success(waterIntake);
        }

        public async Task<Result<bool>> UpdateWaterIntakeAsync(WaterIntake waterIntake)
        {
            _context.WaterIntakes.Update(waterIntake);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteWaterIntakeAsync(WaterIntake waterIntake)
        {
            _context.WaterIntakes.Remove(waterIntake);
            return Result<bool>.Success(true);
        }

        public async Task<Result<List<WaterIntake>>> GetAllWaterIntakesAsync()
        {
            var waterIntakes = await _context.WaterIntakes.ToListAsync();
            return Result<List<WaterIntake>>.Success(waterIntakes);
        }
    }
}
