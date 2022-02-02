using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KlartWebScraperLib
{
    
    public class WebScraper
    {
        
        
        
        
        

        public List<WeatherInfo> GetWeatherInfos(string city, string state, string? date ) // Returns a list of WeatherinfoObjects
        {   
            if (date == null)
                date = DateOnly.FromDateTime(DateTime.Now).ToString();
            string url = UrlReplace(city.ToLower(), state.ToLower(), date);
            return ParseHtml(url);
        }//https://www.klart.se/se/s%C3%B6dermanlands-l%C3%A4n/v%C3%A4der-eskilstuna-gk/
        public string UrlParsing(string city, string state, string date) //Parse just the url, no real reason to use it
        {          
            return UrlReplace(city, state, date);
        }
        string UrlReplace(string city, string state, string date) // replaces special swedish caracters åäö with klart.se specific caracters
        {
            
            Regex å = new Regex("å");
            Regex ä = new Regex("ä");
            Regex ö = new Regex("ö");

            string åReplacement = "%C3%A5"; //Special caracters to Klart.se exclusivly
            string äReplacement = "%C3%A4";
            string öReplacement = "%C3%B6";

            city = å.Replace(city, åReplacement);
            city = ä.Replace(city, äReplacement);
            city = ö.Replace(city, öReplacement);

            state = å.Replace(state, åReplacement);
            state = ä.Replace(state, äReplacement);
            state = ö.Replace(state, öReplacement);

            string url = $"https://www.klart.se/se/{state}-l%C3%A4n/v%C3%A4der-{city}-gk/timmar/#{date}"; //standard string, dont change this if page hasnt changed
            return url;


        }

        List<WeatherInfo> ParseHtml(string url) //Where the info list is created, takes very specific data! If crash or unwanted info, check here first
        {

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            List<WeatherInfo> info = new List<WeatherInfo>();
            Regex dateSearch = new Regex("\\d+-\\d+-\\d+");
            Regex elementsSearch = new Regex(@"\s{1,}");
            int count = 0;
            foreach (var row in doc.DocumentNode.SelectNodes("//div[@class=\"content\"]/*[@class=\"row\"]"))
            {

                string foundDate = dateSearch.Match(row.OuterHtml).Value;
                DateOnly date = DateOnly.Parse(foundDate);
                string[] elements = elementsSearch.Split(row.InnerText.Trim()).ToArray();
                if (elements.Length < 5)
                    continue;
                TimeOnly time = TimeOnly.Parse(elements[0]);
                bool clouds = int.TryParse(elements[10], out int cloudPercentage);
                bool rain = int.TryParse(elements[6], out int rainRisk);
                bool temp = int.TryParse(elements[1].Remove(elements[1].Length-1), out int temperature);
                
                if (temp == true && clouds == true && rain == true)
                {
                    info.Add(new WeatherInfo(date, temperature, time, cloudPercentage, rainRisk));
                }
                else
                {
                    try
                    {
                        info.Add(new WeatherInfo(date, temperature, time));
                    }
                    catch (ArgumentNullException)
                    {

                    }
                    catch (Exception ex)
                    {
                        
                    }
                }   
                count++;
                if (count == 50)
                    break;
            }
            
            if (info.Count > 0)
                return info;
            else
                return null;

        }
    }
}
