using System;
using System.Collections.Generic;

namespace stationProject.Dal.Models;

public partial class Station
{
    public int StationId { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string ManagerName { get; set; } = null!;

    public virtual ICollection<MeasurementsSummary> MeasurementsSummaries { get; } = new List<MeasurementsSummary>();
}
