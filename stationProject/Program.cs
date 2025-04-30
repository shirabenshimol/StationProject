using Bl.Api;
using Bl.Service;
using Dal;
using Dal.Api;
using Dal.Service;
using stationProject.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace stationProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔹 חיבור למסד הנתונים (Database)
            //builder.Services.AddDbContext<DbManager>(options =>
            //    options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\משתמש\\Desktop\\stationProject\\Dal\\DataBase\\DBsql.mdf;Integrated Security=True;Connect Timeout=30"));


            builder.Services.AddDbContext<DbManager>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // 🔹 הוספת השירותים של ה-DAL
            builder.Services.AddScoped<IDal, ServiceDal>();
            builder.Services.AddScoped<IStationDal, ServiceStationDal>();
            builder.Services.AddScoped<IMeasurementsSummaryDal, ServiceMeasurementsSummaryDal>();

            // 🔹 הוספת השירותים של ה-BL
            builder.Services.AddScoped<IStationBl, ServiceStationBl>();

            // 🔹 הוספת ה-Controllers
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            app.UseSwagger();
            app.UseSwaggerUI();
            // 🔹 Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
