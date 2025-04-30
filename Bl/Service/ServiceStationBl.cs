using Bl.Api;
using Bl.Models;
using Dal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bl.Service
{
    public class ServiceStationBl : IStationBl
    {
        private readonly IDal _dal;

        public ServiceStationBl(IDal dal)
        {
            _dal = dal;
        }

        //private List<MeasureBl> LoadMeasurementsFromJson(string stationId)
        //{
        //    List<MeasureBl> measurements = new();
        //    string filename = $"{stationId}.json";

        //    if (!File.Exists(filename))
        //    {
        //        Console.WriteLine($"⚠️ קובץ {filename} לא נמצא");
        //        return measurements;
        //    }

        //    try
        //    {
        //        string content = File.ReadAllText(filename);
        //        var jsonDoc = JsonDocument.Parse(content);

        //        if (!jsonDoc.RootElement.TryGetProperty("Measure", out JsonElement measurementsArray))
        //        {
        //            Console.WriteLine($"⚠️ קובץ {filename} לא מכיל את השדה 'Measure'");
        //            return measurements;
        //        }

        //        measurements = measurementsArray.EnumerateArray().Select(item => new MeasureBl
        //        {
        //            Date = item.GetProperty("MeasurementID").GetInt32(),
        //            MeasureTime = item.GetProperty("MeasurementTime").GetString(),
        //            Temperature = item.GetProperty("Temperature").GetDouble(),
        //            Rainfall = item.GetProperty("Rainfall").GetDouble(),
        //            WindSpeed = item.GetProperty("WindSpeed").GetDouble()
        //        }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"❌ שגיאה בקריאת JSON: {ex.Message}");
        //    }

        //    return measurements;
        //}

        private List<MeasureBl> LoadMeasurementsFromJson(string stationId)
        {
            List<MeasureBl> measurements = new();
            string filename = $"{stationId}.json";

            Console.WriteLine($"📂 טוען קובץ JSON: {filename}");

            if (!File.Exists(filename))
            {
                Console.WriteLine($"⚠️ קובץ {filename} לא נמצא");
                return measurements;
            }

            try
            {
                string content = File.ReadAllText(filename);
                var jsonDoc = JsonDocument.Parse(content);

                if (!jsonDoc.RootElement.TryGetProperty("Measure", out JsonElement measurementsArray))
                {
                    Console.WriteLine($"⚠️ קובץ {filename} לא מכיל את השדה 'Measure'");
                    return measurements;
                }

                measurements = measurementsArray.EnumerateArray().Select(item => new MeasureBl
                {
                    Date = item.GetProperty("MeasurementID").GetInt32(),
                    MeasureTime = item.GetProperty("MeasurementTime").GetString(),
                    Temperature = item.GetProperty("Temperature").GetDouble(),
                    Rainfall = item.GetProperty("Rainfall").GetDouble(),
                    WindSpeed = item.GetProperty("WindSpeed").GetDouble()
                }).ToList();

                Console.WriteLine($"✅ נטענו {measurements.Count} מדידות מהקובץ {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ שגיאה בקריאת JSON: {ex.Message}");
            }

            return measurements;
        }



        //public async Task<List<StationBl>> GetFullData(bool includeMeasurements = true)
        //{
        //    List<StationBl> fullStation = new List<StationBl>();

        //    try
        //    {
        //        var stations = await _dal.StationDalService.Get();

        //        if (stations == null || stations.Count == 0)
        //        {
        //            Console.WriteLine("⚠️ No stations found in the database.");
        //            return fullStation;
        //        }

        //        foreach (var station in stations)
        //        {
        //            List<MeasureBl> measurements = includeMeasurements ? LoadMeasurementsFromJson(station.StationId.ToString()) : new List<MeasureBl>();

        //            fullStation.Add(new StationBl
        //            {
        //                StationId = station.StationId,
        //                Address = station.Address,
        //                City = station.City,
        //                ManagerName = station.ManagerName,
        //                Measurements = measurements
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"❌ שגיאה בעת שליפת הנתונים: {ex.Message}");
        //    }

        //    return fullStation;
        //}
        public async Task<List<StationBl>> GetFullData(bool includeMeasurements = true)
        {
            Console.WriteLine("🚀 התחלת GetFullData");

            List<StationBl> fullStation = new List<StationBl>();

            try
            {
                var stations = await _dal.StationDalService.Get();
                Console.WriteLine($"🔍 נמצאו {stations.Count} תחנות במסד הנתונים");

                if (stations == null || stations.Count == 0)
                {
                    Console.WriteLine("⚠️ אין תחנות במסד הנתונים");
                    return fullStation;
                }

                foreach (var station in stations)
                {
                    Console.WriteLine($"📌 מעבד תחנה ID: {station.StationId}");

                    List<MeasureBl> measurements = includeMeasurements
                        ? LoadMeasurementsFromJson(station.StationId.ToString())
                        : new List<MeasureBl>();

                    fullStation.Add(new StationBl
                    {
                        StationId = station.StationId,
                        Address = station.Address,
                        City = station.City,
                        ManagerName = station.ManagerName,
                        Measurements = measurements
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ שגיאה: {ex.Message}");
            }

            Console.WriteLine("✅ סיום GetFullData");
            return fullStation;
        }


        public async Task<List<MeasurementsSummaryBl>> GetCalculateData()
        {
            var stations = await GetFullData(true);

            var summaries = stations.Select(s => new MeasurementsSummaryBl
            {
                StationId = s.StationId,
                MaxTemperature = s.Measurements.Max(m => m.Temperature),
                MinTemperature = s.Measurements.Min(m => m.Temperature),
                MaxRainfall = s.Measurements.Max(m => m.Rainfall),
                MinRainfall = s.Measurements.Min(m => m.Rainfall)
            }).ToList();

            try
            {
                foreach (var summary in summaries)
                {
                    var summaryEntity = new stationProject.Dal.Models.MeasurementsSummary
                    {
                        StationId = summary.StationId,
                        MaxTemperature = summary.MaxTemperature,
                        MinTemperature = summary.MinTemperature,
                        MaxRainfall = summary.MaxRainfall,
                        MinRainfall = summary.MinRainfall
                    };

                    await _dal.MeasurementsSummaryDalService.Create(summaryEntity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ שגיאה בשמירת נתוני סיכום: {ex.Message}");
            }

            return summaries;
        }

        public async Task Create(StationBl station)
        {
            try
            {
                var stationEntity = new stationProject.Dal.Models.Station
                {
                    StationId = station.StationId,
                    Address = station.Address,
                    City = station.City,
                    ManagerName = station.ManagerName
                };

                await _dal.StationDalService.Create(stationEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ שגיאה ביצירת תחנה: {ex.Message}");
            }
        }

        public async Task<bool> Update(StationBl station)
        {
            try
            {
                var stations = await _dal.StationDalService.Get();
                var existingStation = stations.FirstOrDefault(s => s.StationId == station.StationId);
                if (existingStation == null)
                {
                    Console.WriteLine("⚠️ תחנה לא נמצאה לעדכון.");
                    return false;
                }

                existingStation.Address = station.Address;
                existingStation.City = station.City;
                existingStation.ManagerName = station.ManagerName;

                return await _dal.StationDalService.Update(existingStation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ שגיאה בעדכון התחנה: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(StationBl station)
        {
            try
            {
                var stations = await _dal.StationDalService.Get();
                var existingStation = stations.FirstOrDefault(s => s.StationId == station.StationId);
                if (existingStation == null)
                {
                    Console.WriteLine("⚠️ תחנה לא נמצאה למחיקה.");
                    return false;
                }

                return await _dal.StationDalService.Delete(existingStation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ שגיאה במחיקת התחנה: {ex.Message}");
                return false;
            }
        }
    }
}
