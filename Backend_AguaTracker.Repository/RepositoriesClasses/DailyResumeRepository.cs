using System;
using System.Collections.Generic;
using System.Text;
using Backend_AguaTracker.Repository.Interfaces;
using Backend_AguaTracker.Domain;
using Microsoft.EntityFrameworkCore;


namespace Backend_AguaTracker.Repository.RepositoriesClasses
{
    public class DailyResumeRepository : IDailyResumenRepository
    {
        private readonly AguaTracker_DbContext _context;
        public DailyResumeRepository(AguaTracker_DbContext context)
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
            var dailyResumen = await _context.Set<DailyResume>().FirstOrDefaultAsync(dr => dr.Id == id);
            return dailyResumen != null ? Result<DailyResume>.Success(dailyResumen) : Result<DailyResume>.Failure("No se encontró un resumen diario con el ID especificado.");
        }

        public async Task<Result<bool>> DeleteDailyResumenAsync(DailyResume dailyResumen)
        {
            _context.Set<DailyResume>().Remove(dailyResumen);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateDailyResumenAsync(DailyResume dailyResumen)
        {
            _context.Set<DailyResume>().Update(dailyResumen);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> AddDailyResumenAsync(DailyResume dailyResumen)
        {
            await _context.Set<DailyResume>().AddAsync(dailyResumen);
            return Result<bool>.Success(true);
        }
    }
}
