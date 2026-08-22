using System;
using System.Collections.Generic;
using System.Text;
using Backend_AguaTracker.Repository.Interfaces;

namespace Backend_AguaTracker.Repository.RepositoriesClasses
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AguaTracker_DbContext _context;

        public IUserRepository Users { get; private set; }
        public IWaterIntakeRepository WaterIntakes { get; private set; }
        public IDailyResumenRepository DailyResumes { get; private set; }
        public UnitOfWork
            (
            AguaTracker_DbContext context,
            IUserRepository users,
            IWaterIntakeRepository waterIntakes,
            IDailyResumenRepository dailyResumes
            )
            {
                _context = context;
                Users = users;
                WaterIntakes = waterIntakes;
                DailyResumes = dailyResumes;
            }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
