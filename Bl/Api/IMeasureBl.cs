using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bl.Models;

namespace Bl.Api
{
    public interface IMeasureBl
    {
        List<MeasureBl> Get(string filename);
        List<MeasureBl> GetAllMeasurementsForStation(string stationId);
    }

}
