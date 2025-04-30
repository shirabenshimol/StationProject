using Microsoft.AspNetCore.Mvc;
using Bl.Api;
using Bl.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationBl _stationBl;

        public StationController(IStationBl stationBl)
        {
            _stationBl = stationBl;
        }

        // 🔹 1️⃣ שליפת כל התחנות
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllStations()
        {
            try
            {
                Console.WriteLine("📌 התחלת GetAllStations ב-Controller");
                var stations = await _stationBl.GetFullData(false); // false = בלי מדידות
                return Ok(stations);
            }
            catch (Exception ex)
            {
                return BadRequest($"❌ Error fetching stations: {ex.Message}");
            }
        }

        // 🔹 2️⃣ יצירת תחנה חדשה
        [HttpPost("create")]
        public async Task<IActionResult> CreateStation([FromBody] StationBl station)
        {
            try
            {
                await _stationBl.Create(station);
                return Ok("✅ Station created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"❌ Error creating station: {ex.Message}");
            }
        }

        // 🔹 3️⃣ עדכון תחנה קיימת
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStation(int id, [FromBody] StationBl station)
        {
            try
            {
                bool result = await _stationBl.Update(station);
                if (result)
                    return Ok("✅ Station updated successfully.");
                return NotFound("⚠️ Station not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"❌ Error updating station: {ex.Message}");
            }
        }

        // 🔹 4️⃣ מחיקת תחנה
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            try
            {
                bool result = await _stationBl.Delete(new StationBl { StationId = id });
                if (result)
                    return Ok("✅ Station deleted successfully.");
                return NotFound("⚠️ Station not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"❌ Error deleting station: {ex.Message}");
            }
        }

        // 🔹 5️⃣ שליפת נתוני סיכום לתחנות
        [HttpGet("get-summary")]
        public async Task<IActionResult> GetStationSummary()
        {
            try
            {
                var stations = await _stationBl.GetFullData(true); // true = עם מדידות
                var summaries = await _stationBl.GetCalculateData();
                return Ok(summaries);
            }
            catch (Exception ex)
            {
                return BadRequest($"❌ Error fetching station summary: {ex.Message}");
            }
        }
    }
}
