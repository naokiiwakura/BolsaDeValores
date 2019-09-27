using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;

namespace XOProject.Services.Exchange
{
    public class DistinctItemComparer : IEqualityComparer<HourlyShareRate>
    {

        public bool Equals(HourlyShareRate x, HourlyShareRate y)
        {
            return
                x.TimeStamp == y.TimeStamp &&
                x.Symbol == y.Symbol &&
                x.Rate == y.Rate;
        }

        public int GetHashCode(HourlyShareRate obj)
        {
            return obj.TimeStamp.GetHashCode() ^
                obj.Symbol.GetHashCode() ^
                obj.Rate.GetHashCode();
        }
    }

    public class ShareService : GenericService<HourlyShareRate>, IShareService
    {
        private readonly IShareRepository _shareRepository;

        public ShareService(IShareRepository shareRepository) : base(shareRepository)
        {
            _shareRepository = shareRepository;
        }

        public async Task<IList<HourlyShareRate>> GetBySymbolAsync(string symbol)
        {
            return await EntityRepository
                .Query().Where(x => x.Symbol.Equals(symbol)).Distinct(new DistinctItemComparer()).ToListAsync();
        }

        public async Task<HourlyShareRate> GetHourlyAsync(string symbol, DateTime dateAndHour)
        {
            var date = dateAndHour.Date;
            var hour = dateAndHour.Hour;

            return await EntityRepository
                .Query()
                .Where(x => x.Symbol.Equals(symbol)
                            && x.TimeStamp.Date == date
                            && x.TimeStamp.Hour == hour)
                .SingleOrDefaultAsync();
        }

        public async Task<HourlyShareRate> GetLastPriceAsync(string symbol)
        {
            var share = await EntityRepository
                .Query()
                .Where(x => x.Symbol.Equals(symbol))
                .OrderByDescending(x => x.TimeStamp)
                .FirstOrDefaultAsync();
            return share;
        }

        public async Task<HourlyShareRate> UpdateLastPriceAsync(string symbol, decimal rate)
        {
            var share = await EntityRepository
                .Query()
                .Where(x => x.Symbol.Equals(symbol))
                .OrderByDescending(x => x.TimeStamp)
                .FirstOrDefaultAsync();

            if (share == null)
            {
                return null;
            }

            share.Rate = rate;
            await UpdateAsync(share);

            return share;
        }       
    }
}
