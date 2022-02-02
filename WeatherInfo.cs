using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlartWebScraperLib
{
    public class WeatherInfo
    {
        private DateOnly Date { get; set; }
        private int Temperature { get; set; }
        private TimeOnly Time { get; set; }
        private int? Cloudiness { get; set; }
        private int? RainRisk { get; set; }

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
