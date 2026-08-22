using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_AguaTracker.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IWaterIntakeRepository WaterIntakes { get; }
        IDailyResumenRepository DailyResumes { get; }

        Task<int> CompleteAsync();

    }
}
