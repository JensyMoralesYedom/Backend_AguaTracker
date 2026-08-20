using System;
using System.Collections.Generic;
using System.Text;
using Backend_AguaTracker.Repository.Interfaces;
using Backend_AguaTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Backend_AguaTracker.Repository.RepositoriesClasses
{
    public class DailyResumenRepository : IDailyResumenRepository
    {
        private readonly DbContext _context;
        public DailyResumenRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Result<DailyResume>> GetDailyResumenByDateAsync(DateTime date)
        {
            var dailyResumen = await _context.Set<DailyResume>().FirstOrDefaultAsync(dr => dr.Date.Date == date.Date);
            return dailyResumen != null ? Result<DailyResume>.Success(dailyResumen) : Result<DailyResume>.Failure("No se encontró un resumen diario para la fecha especificada.");
        }

        public async Task<Result<DailyResume>> GetDailyResumenByUserIdAndDateAsync(int userId, DateTime date)
        {
            var dailyResumen = await _context.Set<DailyResume>().FirstOrDefaultAsync(dr => dr.UserId == userId && dr.Date.Date == date.Date);
            return dailyResumen != null ? Result<DailyResume>.Success(dailyResumen) : Result<DailyResume>.Failure("No se encontró un resumen diario para el usuario y fecha especificados.");
        }

        public async Task<Result<List<DailyResume>>> GetDailyResumenByUserIdAsync(int userId)
        {
            var dailyResumens = await _context.Set<DailyResume>().Where(dr => dr.UserId == userId).ToListAsync();
            return Result<List<DailyResume>>.Success(dailyResumens);
        }

        public async Task<Result<List<DailyResume>>> GetAllDailyResumenAsync()
        {
            var dailyResumens = await _context.Set<DailyResume>().ToListAsync();
            return Result<List<DailyResume>>.Success(dailyResumens);
        }

        public async Task<Result<DailyResume>> GetDailyResumenByIdAsync(int id)
        {           
            var dailyResumen = await _context.Set<DailyResume>().FindAsync(id);
            return dailyResumen != null ? Result<DailyResume>.Success(dailyResumen) : Result<DailyResume>.Failure("No se encontró un resumen diario con el ID especificado.");
        }

        public async Task<Result<bool>> DeleteDailyResumenAsync(DailyResume dailyResumen)
        {
            _context.Set<DailyResume>().Remove(dailyResumen);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateDailyResumenAsync(DailyResume dailyResumen)
        {
            _context.Set<DailyResume>().Update(dailyResumen);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> AddDailyResumenAsync(DailyResume dailyResumen)
        {
            await _context.Set<DailyResume>().AddAsync(dailyResumen);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
