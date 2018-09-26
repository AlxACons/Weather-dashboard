using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherBackEndMVC.Helper;
using WeatherBackEndMVC.Data;
using WeatherBackEndMVC.Models;

namespace WeatherBackEndMVC.Controllers
{

    public class HomeController : Controller
    {
        //
        // GET: /Weather/city/{id} optional

        CommonHelper helper = new CommonHelper();

        
        public ActionResult Home(int id = 1,int unit = 1, int date_start = 0, int date_end = 0)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");//allow all access

            List<City> cities = new List<City>();
            HomeModel model = new HomeModel(helper.GetApiPath());
            City city = helper.GetCityLatLon(id.ToString());//view parameter
               
            DateTime date_s;
            DateTime todayDate = DateTime.Now;
            if(date_start < 1)
                date_s = DateTime.Now;
            else
                date_s = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(date_start);//view parameter (1537838213)

            DateTime date_e;
            if (date_end < 1 || date_end < date_start)
                date_e = DateTime.Now;
            else
                date_e = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(date_end);//view parameter (1538090213)

            int days = helper.GetDaysFromTodayMinDateToMaxDate(todayDate, date_e);
            string unit_value = helper.GetTemperatureScale(unit);

            if (city.Lon != 0 && city.Lat != 0)
                cities = GetCityListByDate(model.GetCityTemp(city, unit_value, days), date_s, date_e);

            return Json(cities, JsonRequestBehavior.AllowGet);

        }

        private List<City> GetCityListByDate(List<City> cities,DateTime start,DateTime end)
        {
            List<City> rCities = new List<City>();

            foreach (City city in cities)
            {
                DateTime cityDate = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(city.Ts);
                if (cityDate.Date >= start.Date && cityDate.Date <= end.Date)
                {
                    rCities.Add(city);
                }
            }
            return rCities;
        }

    }
}
