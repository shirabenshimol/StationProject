using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bl.Api;

namespace Bl.Models
{
    public class StationBl
    {
        public int StationId { get; set; }//אםעובד לנסות למחוק

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string ManagerName { get; set; } = null!;

        public List<MeasureBl> Measurements { get; set; } = new();

        
        
    }
}
