using System;
using System.Collections.Generic;
using System.Text;
using Backend_AguaTracker.Repository.Interfaces;
using Backend_AguaTracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_AguaTracker.Repository.RepositoriesClasses
{
    public class DailyResumenRepository : IDailyResumenRepository
    {
        private readonly DbContext _context;
        public DailyResumenRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<DailyResume> GetDailyResumenByDateAsync(DateTime date)
        {
            return await _context.Set<DailyResume>().FirstOrDefaultAsync(dr => dr.Date.Date == date.Date);
        }

        public async Task<DailyResume> GetDailyResumenByUserIdAndDateAsync(int userId, DateTime date)
        {
            return await _context.Set<DailyResume>().FirstOrDefaultAsync(dr => dr.UserId == userId && dr.Date.Date == date.Date);
        }

        public async Task<List<DailyResume>> GetDailyResumenByUserIdAsync(int userId)
        {
            return await _context.Set<DailyResume>().Where(dr => dr.UserId == userId).ToListAsync();
        }

        public async Task<List<DailyResume>> GetAllDailyResumenAsync()
        {
            return await _context.Set<DailyResume>().ToListAsync();
        }

        public async Task<DailyResume> GetDailyResumenByIdAsync(int id)
        {
            return await _context.Set<DailyResume>().FindAsync(id);
        }

        public async Task DeleteDailyResumenAsync(DailyResume dailyResumen)
        {
            _context.Set<DailyResume>().Remove(dailyResumen);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDailyResumenAsync(DailyResume dailyResumen)
        {
            _context.Set<DailyResume>().Update(dailyResumen);
            await _context.SaveChangesAsync();
        }

        public async Task AddDailyResumenAsync(DailyResume dailyResumen)
        {
            await _context.Set<DailyResume>().AddAsync(dailyResumen);
            await _context.SaveChangesAsync();
        }
    }
}
