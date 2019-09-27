using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XOProject.Repository.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace XOProject.Repository.Exchange
{
    public class ShareRepository : GenericRepository<HourlyShareRate>, IShareRepository
    {
        public ShareRepository(ExchangeContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}