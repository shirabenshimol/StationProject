using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Models
{
    public class MeasureBl
    {
        public int Date { get; set; }
        public string MeasureTime { get; set; }
        public double Temperature { get; set; }
        public double Rainfall { get; set; }
        public double WindSpeed { get; set; }
    }
}
