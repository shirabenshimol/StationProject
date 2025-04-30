using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Api;
using Dal.Service;

namespace Dal.Service
{
    public class ServiceDal : IDal
    {
        public IStationDal StationDalService { get; }
        public IMeasurementsSummaryDal MeasurementsSummaryDalService { get; }

        public ServiceDal(IStationDal stationDal, IMeasurementsSummaryDal measurementsSummaryDal)
        {
            StationDalService = stationDal;
            MeasurementsSummaryDalService = measurementsSummaryDal;
        }
    }
}
