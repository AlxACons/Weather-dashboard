using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using WeatherBackEndMVC.Data;

namespace WeatherBackEndMVC.Helper
{
    public class CommonHelper
    {
        private XmlDocument xmldoc = new XmlDocument();
        bool file_loaded = true;
        public CommonHelper()
        {
            try
            {
                this.xmldoc.Load(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
            }
            catch (Exception)
            {
                this.file_loaded = false;
            }
            
        }
        public string GetApiPath()
        {
            string apiPath = string.Empty;

            if (this.file_loaded && xmldoc["Config"]["Requests"]["WeatherApi"] != null)
            {
                apiPath = xmldoc["Config"]["Requests"]["WeatherApi"].InnerText;
            }

            return apiPath;
        }

        public City GetCityLatLon(string id = "1")
        {
            City city = new City();
            if (this.file_loaded && xmldoc["Config"]["WeatherCity"] != null)
            {
                foreach (XmlNode xmlNode in xmldoc["Config"]["WeatherCity"])
                {
                    if (xmlNode["Id"] != null && xmlNode["Lat"] != null && xmlNode["Lon"] != null)
                    {
                        if (id == xmlNode["Id"].InnerText)
                        {
                            city.Lat = float.Parse(xmlNode["Lat"].InnerText);
                            city.Lon = float.Parse(xmlNode["Lon"].InnerText);
                        }
                    }

                }
            }


            return city;
        }


        public int GetDaysFromTodayMinDateToMaxDate(DateTime start_date,DateTime end_date){

            if (start_date.Date < DateTime.Now.Date)
            {
                return 1;
            }
            else
            {
                return (end_date.Date - start_date.Date).Days + 1;// counting from today
            }
            
        }


        public string GetTemperatureScale(int unit)
        {
            string scale = string.Empty;

            if (this.file_loaded && xmldoc["Config"]["TemperatureScales"] != null)
            {
                foreach (XmlNode xmlNode in xmldoc["Config"]["TemperatureScales"])
                {
                    if (xmlNode["Id"] != null && xmlNode["Parameter"] != null)
                    {
                        if (unit == int.Parse(xmlNode["Id"].InnerText) )
                        {
                            scale = xmlNode["Parameter"].InnerText;
                        }
                    }

                }
            }
            return scale;
        }

    }
}