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

        public async Task AddWaterIntakeAsync(WaterIntake waterIntake)
        {
            _context.WaterIntakes.Add(waterIntake);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WaterIntake>> GetWaterIntakesByUserIdAsync(int userId)
        {
            return await _context.WaterIntakes
                .Where(wi => wi.UserId == userId)
                .ToListAsync();
        }

        public async Task<WaterIntake> GetWaterIntakeByIdAsync(int id)
        {
            return await _context.WaterIntakes.FirstOrDefaultAsync(wi => wi.Id == id);
        }

        public async Task UpdateWaterIntakeAsync(WaterIntake waterIntake)
        {
            _context.WaterIntakes.Update(waterIntake);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWaterIntakeAsync(WaterIntake waterIntake)
        {
            _context.WaterIntakes.Remove(waterIntake);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WaterIntake>> GetAllWaterIntakesAsync()
        {
            return await _context.WaterIntakes.ToListAsync();
        }
    }
}
