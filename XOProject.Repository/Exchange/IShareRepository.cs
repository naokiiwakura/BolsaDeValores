﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XOProject.Repository.Domain;

namespace XOProject.Repository.Exchange
{
    public interface IShareRepository : IGenericRepository<HourlyShareRate>
    {
    }
}