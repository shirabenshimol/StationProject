using Dal.Api;
using Microsoft.EntityFrameworkCore;
using stationProject.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Service
{
    
    public class ServiceStationDal : IStationDal
    {
        DbManager data;
        public ServiceStationDal(DbManager d)
        {
            data = d;
        }
        public void print(Station s)
        {
            s.ToString();
        }
        //public async Task Create(Station s) 
        //{
        //    if (await data.Stations.FindAsync(s.StationId) == null) 
        //    {
        //        await data.Stations.AddAsync(s); 
        //        await data.SaveChangesAsync(); 
        //        Console.WriteLine("תחנה נוספה בהצלחה");
        //    }
        //}

        public async Task Create(Station s)
        {
            if (await data.Stations.FindAsync(s.StationId) != null)
            {
                throw new Exception("⚠️ תחנה כבר קיימת במסד.");
            }

            await data.Stations.AddAsync(s);
            await data.SaveChangesAsync();
            Console.WriteLine("תחנה נוספה בהצלחה");
        }


        //public async Task<List<Station>> Get()
        //{
        //    return await Task.Run(() => data.Stations.ToListAsync());
        //}
        public async Task<List<Station>> Get()
        {
            Console.WriteLine("📌 EF מחובר אל: " + data.Database.GetDbConnection().ConnectionString);

            Console.WriteLine("🚀 ServiceStationDal: מתחיל שליפת תחנות מהמסד");
            var result = await data.Stations.ToListAsync(); // ✅ שולף נתונים מהמסד
            Console.WriteLine($"✅ ServiceStationDal: נמצאו {result.Count} תחנות");
            return result;
        }


        public async Task<List<Station>> Search(Expression<Func<Station, bool>> predicate)
        {
            return await data.Stations.Where(predicate).ToListAsync();
        }

        public async Task<bool> Delete(Station s)
        {
            try
            {
                var station = await data.Stations.FindAsync(s.StationId);
                if (station == null)
                {
                    Console.WriteLine("לא נמצאה תחנה למחיקה.");
                    return false;
                }

                data.Stations.Remove(station);
                await data.SaveChangesAsync(); // ✅ שמירה אסינכרונית
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה במחיקת התחנה: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Update(Station s)
        {
            try
            {
                var existingStation = await data.Stations.FindAsync(s.StationId);
                if (existingStation == null)
                {
                    Console.WriteLine("תחנה לא נמצאה.");
                    return false;
                }

                existingStation.Address = s.Address;
                existingStation.City = s.City;
                existingStation.ManagerName = s.ManagerName;

                data.Stations.Update(existingStation);
                await data.SaveChangesAsync(); // ✅ שמירה אסינכרונית
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה בעדכון התחנה: {ex.Message}");
                return false;
            }
        }

    }
}

