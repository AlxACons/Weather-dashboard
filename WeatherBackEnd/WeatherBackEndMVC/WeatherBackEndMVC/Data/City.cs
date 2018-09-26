using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherBackEndMVC.Data
{
    public class City
    {
        private int ts = 0;//timestamp
        private float lat = 0;
        private float lon = 0;
        private float max_temp = 0;
        private float min_temp = 0;

        public int Ts
        {
            get { return ts; }
            set { ts = value; }
        }
        public float Lat
        {
            get { return lat; }
            set { lat = value; }
        }
        public float Lon
        {
            get { return lon; }
            set { lon = value; }
        }

        public float Max_temp
        {
            get { return max_temp; }
            set { max_temp = value; }
        }

        public float Min_temp
        {
            get { return min_temp; }
            set { min_temp = value; }
        }

    }
}