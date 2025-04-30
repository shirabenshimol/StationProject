using System;
using System.Collections.Generic;

namespace stationProject.Dal.Models;

public partial class MeasurementsSummary
{
    public int SummaryId { get; set; }

    public int StationId { get; set; }

    public double MaxTemperature { get; set; }

    public double MinTemperature { get; set; }

    public double MaxRainfall { get; set; }

    public double MinRainfall { get; set; }

    public virtual Station Station { get; set; } = null!;
}
