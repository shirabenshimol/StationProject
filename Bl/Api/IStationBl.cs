using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bl.Models;
using Dal.Service;
using stationProject.Dal.Models;

namespace Bl.Api
   
{
    public interface IStationBl
    {
       Task<List<StationBl>>GetFullData(bool includeMeasurements);
       Task< List<MeasurementsSummaryBl>> GetCalculateData();
        Task Create(StationBl item);
        Task<bool >Update(StationBl station);
        Task<bool> Delete(StationBl station);
    }



}
