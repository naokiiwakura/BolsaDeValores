using System;
using System.Threading.Tasks;
using XOProject.Repository.Domain;
using XOProject.Services.Domain;

namespace XOProject.Services.Exchange
{
    public interface IAnalyticsService : IGenericService<HourlyShareRate>
    {
        Task<AnalyticsPrice> GetDailyAsync(string symbol, int year, int month, int day);
        Task<AnalyticsPrice> GetWeeklyAsync(string symbol, int year, int week);
        Task<AnalyticsPrice> GetMonthlyAsync(string symbol, int year, int month);
    }
}
