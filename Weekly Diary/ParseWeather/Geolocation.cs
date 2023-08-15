using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Weekly_Diary.ParseWeather
{
    public class Geolocation
    {
        public void  GetGeolocation()
        {
            //  Geolocation res = new Geolocation();
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            // GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            /*  watcher.TryStart(false, TimeSpan.FromSeconds(3));

              if (!watcher.Position.Location.IsUnknown)
              {
                  res.Latitude = watcher.Position.Location.Latitude;
                  res.Longtitude = watcher.Position.Location.Longitude;
              }

              return res;
          }

          public double Latitude { get; private set; }
          public double Longtitude { get; private set; }*/




            var whereat = watcher.Position.Location;

            var Lat = whereat.Latitude.ToString("0.000000");
            var Lon = whereat.Longitude.ToString("0.000000");


            //optional parameters for future use
            whereat.Altitude.ToString();
            whereat.HorizontalAccuracy.ToString();
            whereat.VerticalAccuracy.ToString();
            whereat.Course.ToString();
            whereat.Speed.ToString();

            MessageBox.Show(string.Format("Lat: {0}\nLon: {1}", Lat, Lon));


        }
    }
}