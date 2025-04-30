using System.Collections.Generic;
using System.Threading.Tasks;
using stationProject.Dal.Models;

namespace Dal.Api
{
    public interface IMeasurementsSummaryDal
    {
        Task Create(MeasurementsSummary summary);
        Task<List<MeasurementsSummary>> GetAll();
        Task<List<MeasurementsSummary>> GetByStationId(int stationId);
    }
}
