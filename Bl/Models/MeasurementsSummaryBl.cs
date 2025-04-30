using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class MeasurementsSummaryBl
    {
        public int SummaryId { get; set; }

        public int StationId { get; set; }

        public double MaxTemperature { get; set; }

        public double MinTemperature { get; set; }

        public double MaxRainfall { get; set; }

        public double MinRainfall { get; set; }

        public virtual StationBl Station { get; set; } = null!;
    }
}
