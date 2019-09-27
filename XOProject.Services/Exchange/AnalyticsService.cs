using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;
using XOProject.Services.Domain;

namespace XOProject.Services.Exchange
{
    public class AnalyticsService : GenericService<HourlyShareRate>, IAnalyticsService
    {
        private readonly IShareRepository _shareRepository;

        public AnalyticsService(IShareRepository shareRepository) : base(shareRepository)
        {
            _shareRepository = shareRepository;
        }

        public async Task<AnalyticsPrice> GetDailyAsync(string symbol, int year, int month, int day)
        {
            // TODO: Add implementation for the daily summary
            var listOfSharesByDay = await EntityRepository
                .Query()
                .Where(x => x.Symbol.Equals(symbol)
                            && x.TimeStamp.Day == day
                            && x.TimeStamp.Month == month
                            && x.TimeStamp.Year == year)

                .ToListAsync();

            var result = new AnalyticsPrice
            {
                Open = listOfSharesByDay.OrderBy(p => p.TimeStamp).FirstOrDefault().Rate,
                High = listOfSharesByDay.Max(p => p.Rate),
                Low = listOfSharesByDay.Min(p => p.Rate),
                Close = listOfSharesByDay.OrderByDescending(p => p.TimeStamp).FirstOrDefault().Rate,
            };

            return result;
        }

        public async Task<AnalyticsPrice> GetWeeklyAsync(string symbol, int year, int week)
        {
            // TODO: Add implementation for the weekly summary
            var listOfSharesByDay = await EntityRepository
                .Query()
                .Where(x => x.Symbol.Equals(symbol)
                            && x.TimeStamp.Year == year
                            && Week(x.TimeStamp) == week)

                .ToListAsync();

            var result = new AnalyticsPrice
            {
                Open = listOfSharesByDay.OrderBy(p => p.TimeStamp).FirstOrDefault().Rate,
                High = listOfSharesByDay.Max(p => p.Rate),
                Low = listOfSharesByDay.Min(p => p.Rate),
                Close = listOfSharesByDay.OrderByDescending(p => p.TimeStamp).FirstOrDefault().Rate,
            };

            return result;
        }

        public async Task<AnalyticsPrice> GetMonthlyAsync(string symbol, int year, int month)
        {
            // TODO: Add implementation for the monthly summary
            var listOfSharesByMonth = await EntityRepository
                .Query()
                .Where(x => x.Symbol.Equals(symbol)
                            && x.TimeStamp.Month == month
                            && x.TimeStamp.Year == year)

                .ToListAsync();

            var result = new AnalyticsPrice
            {
                Open = listOfSharesByMonth.OrderBy(p => p.TimeStamp).FirstOrDefault().Rate,
                High = listOfSharesByMonth.Max(p => p.Rate),
                Low = listOfSharesByMonth.Min(p => p.Rate),
                Close = listOfSharesByMonth.OrderByDescending(p => p.TimeStamp).FirstOrDefault().Rate,
            };

            return result;
        }

        private int Week(DateTime? nullable)
        {
            if (nullable.HasValue)
            {
                GregorianCalendar gCalendar = new GregorianCalendar();
                int WeekNumber = gCalendar.GetWeekOfYear(nullable.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                return WeekNumber;
            }
            else
                return 0;
        }
    }
}