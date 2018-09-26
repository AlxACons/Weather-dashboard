using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using WeatherBackEndMVC.Data;
using Newtonsoft.Json;

namespace WeatherBackEndMVC.Models
{
    public class HomeModel
    {
        string apiPath = string.Empty;

        public HomeModel(string path)
        {
            this.apiPath = path;
        }

        public List<City> GetCityTemp(City city, string unit, int days = 1)
        {
            List<City> cities = new List<City>();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.apiPath + "&lat=" + city.Lat + "&lon=" + city.Lon + "&days=" + days.ToString() + "&units=" + unit);
                request.UseDefaultCredentials = true;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    dynamic json = JsonConvert.DeserializeObject(sr.ReadToEnd());
                    foreach (dynamic item in json["data"])
                    {
                        cities.Add(
                            new City()
                            {
                                Ts = (int)item["ts"],
                                Max_temp = (float)item["max_temp"],
                                Min_temp = (float)item["min_temp"],
                                Lat = city.Lat,
                                Lon = city.Lon
                            }
                        );
                    }
                }
               
            }
            catch (Exception)
            {
                cities = new List<City>();
            }
            return cities;
           


        }
    }
}