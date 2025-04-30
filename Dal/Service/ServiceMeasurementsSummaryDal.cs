using Dal.Api;
using Microsoft.EntityFrameworkCore;
using stationProject.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Service
{
    public class ServiceMeasurementsSummaryDal : IMeasurementsSummaryDal
    {
        private readonly DbManager _db;

        public ServiceMeasurementsSummaryDal(DbManager db)
        {
            _db = db;
        }

        public async Task Create(MeasurementsSummary summary)
        {
            await _db.MeasurementsSummaries.AddAsync(summary);
            await _db.SaveChangesAsync();
        }

        public async Task<List<MeasurementsSummary>> GetAll()
        {
            return await _db.MeasurementsSummaries.ToListAsync();
        }

        public async Task< List<MeasurementsSummary>> GetByStationId(int stationId)
        {
            return await _db.MeasurementsSummaries.Where(s => s.StationId == stationId).ToListAsync();
        }
    }
}
