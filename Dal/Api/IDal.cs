﻿using Dal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IDal
    {
    
        public IStationDal StationDalService { get; }
        IMeasurementsSummaryDal MeasurementsSummaryDalService { get; }
    }
}
