using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlartWebScraperLib
{
    public class WeatherInfo
    {
        public DateOnly Date { get; set; }
        public int Temperature { get; set; }
        public TimeOnly Time { get; set; }
        public int Cloudiness { get; set; }
        public int RainRisk { get; set; }

        public WeatherInfo(DateOnly date, int Temp, TimeOnly time, int cloudiness, int rainRisk) 
        {
            Date = date;
            Temperature = Temp;
            Time = time;
            Cloudiness = cloudiness;
            RainRisk = rainRisk;

        }
        public WeatherInfo(DateOnly date, int Temp, TimeOnly time) 
        {
            Date = date;
            Temperature = Temp;
            Time = time;
        }
        public WeatherInfo GetAll()
        {
            return this;
        }




}
}
