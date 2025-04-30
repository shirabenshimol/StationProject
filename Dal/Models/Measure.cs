using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class Measure
    {
        public int Date { get; set; }
        public string MeasureTime { get; set; }
        public double Temperature { get; set; }
        public double Rainfall { get; set; }
        public double WindSpeed { get; set; }


        //public Measure(int Date, string MeasureTime, double Temperature, double Rainfall, double WindSpeed)
        //{
        //    this.Date = Date;
        //    this.MeasureTime = MeasureTime;
        //    this.Temperature = Temperature;
        //    this.Rainfall = Rainfall;
        //    this.WindSpeed = WindSpeed;
        //}


    }
}
