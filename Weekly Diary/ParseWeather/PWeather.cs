using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Weekly_Diary.ParseWeather
{
      class PWeather
    {
        public string Parse()
        {
            WebRequest request = WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?q=Biala Podlaska&APPID=3da0baf6af9aff062bdc4aeb2e321174");
            request.Method = "POST";
            request.ContentType = "application/x-www-urlencoded";

            WebResponse response = request.GetResponse();
            string answer = string.Empty;

            using(Stream s = response.GetResponseStream())
            {
                using(StreamReader reader = new StreamReader(s))
                {
                    answer = reader.ReadToEnd();
                }
            }
            response.Close();
            return answer;
        }
        
        
    }
}
